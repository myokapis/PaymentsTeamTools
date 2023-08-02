<!DOCTYPE html>
<html>
<head>
  <meta charset="utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Cardflight Settlement Tracker</title>
  <!--<link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/smoothness/jquery-ui.css" />-->
  <link rel="stylesheet" href="css/site.css" />
  <link rel="stylesheet" href="css/master.css" />
  <script src="https://cdnjs.cloudflare.com/ajax/libs/cash/8.1.5/cash.min.js"></script>
  <script src="js/master.js" type="text/javascript"></script>
  @@HEAD@@
</head>
<body>
  <div class="body-div">
    <div class="main-div">
      <div class="nav-button-container">
        <input type="button" id="home-tab" class="nav-button nav-button-selected" value="Home" data-action="home" data-filter-mask="1" />
        <input type="button" id="files-tab" class="nav-button nav-button-unselected" value="Files" data-action="files" data-filter-mask="2" />
        <input type="button" id="transactions-tab" class="nav-button nav-button-unselected" value="Transactions" data-action="transactions" data-filter-mask="4" />
        <input type="button" id="details-tab" class="nav-button nav-button-unselected" value="Details" data-action="details" data-filter-mask="0" />
        <input type="button" id="about-tab" class="nav-button nav-button-unselected" value="About" data-action="about" data-filter-mask="0" />
      </div>
      @@BODY@@
    </div>
    <form id="session-scope" action="" method="post" class="session-scope">
      <div id="filter-div">
        <h3>Filters</h3>

        <div class="selector-row-div">
          <label for="date-from" class="selector-label">Date From</label>
          <input type="date" id="date-from" name="sessionScope.DateFrom" class="date-picker" data-filter-mask="6" value="@@DateFrom@@" />
        </div>
        <div class="selector-row-div">
          <label for="date-to" class="selector-label">Date To</label>
          <input type="date" id="date-to" name="sessionScope.DateTo" class="date-picker" data-filter-mask="6" value="@@DateTo@@" />
        </div>
        <div class="selector-row-div">
          <div>
            <label for="merchant-key" class="selector-label">Merchant</label>
            <select id="merchant-key" name="sessionScope.MerchantAccountId" data-filter-mask="6">
              <!-- @@MERCHANT_KEY@@ -->
              <option value="@@VALUE@@" @@SELECTED@@>@@TEXT@@</option>
              <!-- @@MERCHANT_KEY@@ -->
            </select>
            <select id="merchant-name" name="sessionScope.MerchantAccountId" data-filter-mask="6" style="display: none">
              <!-- @@MERCHANT_NAME@@ -->
              <option value="@@VALUE@@" @@SELECTED@@>@@TEXT@@</option>
              <!-- @@MERCHANT_NAME@@ -->
            </select>
          </div>
          <div class="merchant-radio-div">
            <input type="radio" id="merchant-key-radio" name="merchant-selector" data-filter-mask="6" value="merchant-key" checked />
            <label for="merchant_key-radio">Key</label>
            <input type="radio" id="merchant-name-radio" name="merchant-selector" data-filter-mask="6" value="merchant-name" />
            <label for="merchant-name-radio">Name</label>
          </div>
        </div>
        <div class="selector-row-div">
          <label for="transaction-state" class="selector-label">Transaction State</label>
          <select id="transaction-state" name="sessionScope.TransactionState" data-filter-mask="4">
            <!-- @@TRANSACTION_STATE@@ -->
            <option value="@@VALUE@@" @@SELECTED@@>@@TEXT@@</option>
            <!-- @@TRANSACTION_STATE@@ -->
          </select>
        </div>
        <div class="selector-row-div">
          <label for="transaction-status" class="selector-label">Transaction Status</label>
          <select id="transaction-status" name="sessionScope.TransactionStatus" data-filter-mask="4">
            <!-- @@TRANSACTION_STATUS@@ -->
            <option value="@@VALUE@@" @@SELECTED@@>@@TEXT@@</option>
            <!-- @@TRANSACTION_STATUS@@ -->
          </select>
        </div>
        <div class="selector-row-div">
          <label for="transaction-amount" class="selector-label">Transaction Amount</label>
          <input type="number" id="transaction-amount" name="sessionScope.TransactionAmount" data-filter-mask="4" step="0.01" min="0" value="@@TransactionAmount@@" />
        </div>
        <div class="selector-row-div">
          <button type="button" id="apply-filter" data-filter-mask="6" value="Apply Filter">Apply Filter</button>
        </div>
      </div>
      <div id="selection-div">
        <div class="selector-row-div">
          <input type="hidden" id="file-id" name="sessionScope.SettlementFileId" />
        </div>
        <div class="selector-row-div">
          <input type="hidden" id="transaction-id" name="sessionScope.TransactionId" readonly />
        </div>
      </div>
    </form>
  </div>
  <div class="footer-div">
    <div>Copyright 2023 Cardflight</div>
  </div>
  @@TAIL@@
  <dialog id="errorDialog">
    <div id="errorContent">
      <div id="errorCaption">Error</div>
      <div id="errorMessage">An error occurred during the last operation.</div>
      <button id="closeDialog">Close</button>
    </div>
  </dialog>
</body>
</html>