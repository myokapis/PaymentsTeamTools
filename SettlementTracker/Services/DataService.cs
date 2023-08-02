using TemplateEngine.Web;
using SettlementTracker.Models;

namespace SettlementTracker.Services
{

    public class DataService : IDataService
    {
        private static readonly Random random = Random.Shared;
        public IList<DailySummary> GetDailySummaries()
        {
            var startDate = DateTime.Now.AddDays(-4).Date;
            var endDate = DateTime.Now.AddDays(1);

            bool filter(DailySummary summary)
            {
                return summary.Date >= startDate
                && summary.Date < endDate;
            }
            return summaries.Where(s => filter(s)).ToList();
        }

        public Details GetDetails(SessionScope sessionScope)
        {
            var transaction = transactions.First(t => t.Id == sessionScope.TransactionId);

            return new Details
            {
                CommonGroup = new CommonGroup
                {
                    CardCaptCap = "0",
                    GroupId = "10001",
                    LocalDateTime = "20230523135516",
                    MerchId = "840085478",
                    OrderNum = "1344",
                    POSCondCode = "59",
                    POSEntryMode = "012",
                    PymtType = "Credit",
                    RefNum = "1608795264",
                    ReversalInd = "",
                    SettleInd = "1",
                    STAN = "267690",
                    TermCatCode = "00",
                    TermEntryCapablt = "01",
                    TermId = "00000001",
                    TermLocInd = "1",
                    TPPID = "MNY001",
                    TrnmsnDateTime = "20230523135414",
                    TxnAmount = (transaction.Amount * 100).ToString(),
                    TxnCrncy = "840",
                    TxnType = "Authorization"
                },
                ECommGroup = new ECommGroup
                {
                    CustSvcPhoneNumber = "8008888088",
                    ECommTxnInd = "03"
                },
                VisaGroup = new VisaGroup
                {
                    ACI = "N",
                    TransId = "12589754012346858IV"
                }
            };

        }

        public IList<SettlementFile> GetFiles(SessionScope sessionScope)
        {
            var query = settlementFiles.Where(f => f.SettlementFileDate >= sessionScope.DateFrom.Date
                && f.SettlementFileDate < sessionScope.DateTo.Date.AddDays(1));

            if (sessionScope.TransactionId.HasValue)
            {
                var settlementFile = transactions
                    .FirstOrDefault(t => t.Id == sessionScope.TransactionId.Value);

                query = query.Where(file => file.SettlementFileId == settlementFile?.SettlementFileId);
            }
            else if (sessionScope.SettlementFileId.HasValue)
            {
                query = query.Where(file => file.SettlementFileId == sessionScope.SettlementFileId);
            }
            else if (sessionScope.MerchantAccountId.HasValue)
            {
                var merchantFileIds = settlementFileMerchants
                    .Where(f => f.MerchantAccountId == sessionScope.MerchantAccountId.Value)
                    .Select(f => f.SettlementFileId);

                query = query.Where(file => merchantFileIds.Any(fileId => fileId == file.SettlementFileId));
            }

            return query.ToList();
        }

        public IList<Merchant> GetMerchants(SessionScope sessionScope)
        {
            bool filter(Merchant merchant)
            {
                return (sessionScope.MerchantAccountId == null) || (merchant.AccountId == sessionScope.MerchantAccountId);
            }

            return merchants.Where(m => filter(m)).ToList();
        }

        /// <summary>
        /// A dummy helper method to provide a default page scope
        /// </summary>
        /// <typeparam name="T">The object type containing the page scope</typeparam>
        /// <param name="mappingFunc">A function for mapping the provided object type to a page scope object</param>
        /// <returns>An object of type T representing the page scope</returns>
        public SessionScope GetSessionScope()
        {
            // TODO: in the real app these times need to be UTC?
            return new SessionScope
            {
                DateFrom = DateTime.Now.Date.AddDays(-5),
                DateTo = DateTime.Now.Date
            };
        }

        public IList<Transaction> GetTransactions(SessionScope sessionScope)
        {
            IEnumerable<Transaction> query;

            if (sessionScope.TransactionId.HasValue)
            {
                query = transactions.Where(transaction => transaction.Id == sessionScope.TransactionId);
            }
            else
            {
                var files = settlementFiles.Where(file => (file.SettlementFileDate >= sessionScope.DateFrom)
                        && (file.SettlementFileDate < sessionScope.DateTo.AddDays(1)));

                if (sessionScope.SettlementFileId.HasValue)
                {
                    files = files.Where(file => file.SettlementFileId == sessionScope.SettlementFileId);
                }
                else if(sessionScope.MerchantAccountId.HasValue)
                {
                    files = files.Where(file => settlementFileMerchants.Any(merchantFile =>
                            (merchantFile.MerchantAccountId == sessionScope.MerchantAccountId)
                            && (merchantFile.SettlementFileId == file.SettlementFileId)
                        ));
                }

                query = transactions.Where(transaction =>
                    files.Any(file => file.SettlementFileId == transaction.SettlementFileId)
                    && ((sessionScope.TransactionAmount == null) || (transaction.Amount == sessionScope.TransactionAmount))
                    && ((sessionScope.TransactionState == null) || (transaction.State == sessionScope.TransactionState))
                    && ((sessionScope.TransactionStatus == null) || (transaction.Status == sessionScope.TransactionStatus)));
            }

            // In the real app the query will need to be limited to a reasonable number or records
            return query.Take(50).ToList();
        }

        public IList<Option> GetTransactionStates()
        {
            return transactionStates;
        }

        public IList<Option> GetTransactionStatuses()
        {
            return transactionStatuses;
        }

        private static DailySummary GetDailySummary(int daysAgo)
        {
            return new DailySummary
            {
                FileCount = random.Next(1, 11),
                MerchantCount = random.Next(100, 250),
                PendingCount = random.Next(5000, 25000),
                RejectedCount = random.Next(10),
                SettledCount = random.Next(5000, 25000),
                Date = DateTime.Today.AddDays(daysAgo),
                UnsentCount = random.Next(14)
            };
        }

        private static readonly List<Merchant> merchants = new()
            {
                new Merchant
                {
                    AccountId = 11,
                     AccountKey = "acc_34563445",
                     Name = "Tom's Hardware"
                },
                new Merchant
                {
                    AccountId = 12,
                     AccountKey = "acc_5512489",
                     Name = "The Waltz Room"
                },
                new Merchant
                {
                    AccountId = 13,
                     AccountKey = "acc_8894567",
                     Name = "Barber Shoppe"
                },
                new Merchant
                {
                    AccountId = 14,
                     AccountKey = "acc_8815879",
                     Name = "Plumb Us"
                }
            };

        private static readonly List<SettlementFile> settlementFiles = new()
        {
            new SettlementFile
                {
                    SettlementFileId = 1,
                    SettlementFileDate = DateTime.Now.AddDays(-2),
                    SettlementFileName = $"PTSFile.{DateTime.Now.AddDays(-2):yyyy-MM-dd}.134425.txt.pgp",
                    AcknowledgementFileId = 11,
                    AcknowledgementFileName = $"PTSFile.{DateTime.Now.AddDays(-2):yyyy-MM-dd}.134425.txt.pgp",
                    AcknowledgementFileDate = DateTime.Now.AddDays(-2)
            },
                new SettlementFile
                {
                    SettlementFileId = 2,
                    SettlementFileDate = DateTime.Now.AddDays(-2),
                    SettlementFileName = $"PTSFile.{DateTime.Now.AddDays(-2):yyyy-MM-dd}.175618.txt.pgp",
                    AcknowledgementFileId = 12,
                    AcknowledgementFileName = $"PTSFile.{DateTime.Now.AddDays(-1):yyyy-MM-dd}.175618.txt.pgp",
                    AcknowledgementFileDate = DateTime.Now.AddDays(-1)
                },
                new SettlementFile
                {
                    SettlementFileId = 3,
                    SettlementFileDate = DateTime.Now.AddDays(-1),
                    SettlementFileName = $"PTSFile.{DateTime.Now.AddDays(-1):yyyy-MM-dd}.090814.txt.pgp",
                    AcknowledgementFileId = 13,
                    AcknowledgementFileName = $"PTSFile.{DateTime.Now:yyyy-MM-dd}.090814.txt.pgp",
                    AcknowledgementFileDate = DateTime.Now
                },
                new SettlementFile
                {
                    SettlementFileId = 4,
                    SettlementFileDate = DateTime.Now,
                    SettlementFileName = $"PTSFile.{DateTime.Now:yyyy-MM-dd}.031548.txt.pgp"
                }
        };

        private static readonly List<SettlementFileMerchant> settlementFileMerchants = new()
        {
                new SettlementFileMerchant
                {
                    SettlementFileId = 1,
                    MerchantAccountId = 11,
                    MerchantKey = "acc_34563445",
                    MerchantName = "Tom's Hardware"
                },
                new SettlementFileMerchant
                {
                    SettlementFileId = 2,
                    MerchantAccountId = 12,
                    MerchantKey = "acc_5512489",
                    MerchantName = "The Waltz Room"
                },
                new SettlementFileMerchant
                {
                    SettlementFileId = 3,
                    MerchantAccountId = 13,
                    MerchantKey = "acc_8894567",
                    MerchantName = "Barber Shoppe"
                },
                new SettlementFileMerchant
                {
                    SettlementFileId = 4,
                    MerchantAccountId = 14,
                    MerchantKey = "acc_8815879",
                    MerchantName = "Plumb Us"
                }
        };

        private static readonly List<DailySummary> summaries = new()
            {
                GetDailySummary(0),
                GetDailySummary(-1),
                GetDailySummary(-2),
                GetDailySummary(-3),
                GetDailySummary(-4)
            };

        private static readonly List<Transaction> transactions = new()
        {
            new Transaction
            {
                Amount = 33.47,
                ChargeId = 14,
                CreatedDate = DateTime.Now.AddDays(-2),
                Id = 1,
                MerchantAccountId = 11,
                RecordNo = 347,
                RefundId = null,
                SettlementFileId = 1,
                State = 800,
                Status = 300
            },
            new Transaction
            {
                Amount = 149.83,
                ChargeId = 17,
                CreatedDate = DateTime.Now.AddDays(-2),
                Id = 2,
                MerchantAccountId = 11,
                RecordNo = 356,
                RefundId = null,
                SettlementFileId = 1,
                State = 800,
                Status = 300
            },
            new Transaction
            {
                Amount = 22.39,
                ChargeId = 111,
                CreatedDate = DateTime.Now.AddDays(-2),
                Id = 3,
                MerchantAccountId = 12,
                RecordNo = 13,
                RefundId = null,
                SettlementFileId = 2,
                State = 800,
                Status = 300
            },
            new Transaction
            {
                Amount = 8.47,
                ChargeId = 114,
                CreatedDate = DateTime.Now.AddDays(-2),
                Id = 4,
                MerchantAccountId = 12,
                RecordNo = 44,
                RefundId = null,
                SettlementFileId = 2,
                State = 800,
                Status = 350
            },
            new Transaction
            {
                Amount = 11.19,
                ChargeId = 117,
                CreatedDate = DateTime.Now.AddDays(-2),
                Id = 5,
                MerchantAccountId = 12,
                RecordNo = 62,
                RefundId = null,
                SettlementFileId = 2,
                State = 800,
                Status = 300
            },
            new Transaction
            {
                Amount = 78.21,
                ChargeId = 123,
                CreatedDate = DateTime.Now.AddDays(-1),
                Id = 6,
                MerchantAccountId = 13,
                RecordNo = 74,
                RefundId = null,
                SettlementFileId = 3,
                State = 800,
                Status = 300
            },
            new Transaction
            {
                Amount = 44.57,
                ChargeId = 237,
                CreatedDate = DateTime.Now.AddDays(-1),
                Id = 7,
                MerchantAccountId = 13,
                RecordNo = 103,
                RefundId = null,
                SettlementFileId = 3,
                State = 1800,
                Status = 350
            },
            new Transaction
            {
                Amount = 10.00,
                ChargeId = 245,
                CreatedDate = DateTime.Now.AddDays(-1),
                Id = 8,
                MerchantAccountId = 13,
                RecordNo = 122,
                RefundId = null,
                SettlementFileId = 3,
                State = 800,
                Status = 300
            },
            new Transaction
            {
                Amount = 19.19,
                ChargeId = 358,
                CreatedDate = DateTime.Now,
                Id = 9,
                MerchantAccountId = 14,
                RecordNo = 12,
                RefundId = null,
                SettlementFileId = 4,
                State = 1800,
                Status = null
            },
            new Transaction
            {
                Amount = 23.87,
                ChargeId = 404,
                CreatedDate = DateTime.Now,
                Id = 10,
                MerchantAccountId = 14,
                RecordNo = 26,
                RefundId = null,
                SettlementFileId = 4,
                State = 1800,
                Status = null
            }
        };

        private static readonly List<Option> transactionStates = new()
        {
            new Option { Text = "", Value = "" },
            new Option { Text = "Approved", Value = "200" },
            new Option { Text = "Pending", Value = "1800" },
            new Option { Text = "Settled", Value = "800" }
        };

        private static readonly List<Option> transactionStatuses = new()
        {
            new Option { Text = "", Value = "" },
            new Option { Text = "Abandoned", Value = "400" },
            new Option { Text = "Accepted", Value = "300" },
            new Option { Text = "Error", Value = "100" },
            new Option { Text = "Pending", Value = "0" },
            new Option { Text = "Rejected", Value = "350" },
            new Option { Text = "Sent", Value = "200" }
        };
    }

}
