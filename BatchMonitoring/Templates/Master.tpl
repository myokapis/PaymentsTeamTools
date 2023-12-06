<!DOCTYPE html>
<html>
<head>
  <meta charset="utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Cardflight Settlement Tracker</title>
  <link rel="stylesheet" href="css/site.css" />
  <link rel="stylesheet" href="css/master.css" />
  <script src="https://cdnjs.cloudflare.com/ajax/libs/cash/8.1.5/cash.min.js"></script>
  <script src="js/master.js" type="text/javascript"></script>
  @@HEAD@@
</head>
<body>
  <div class="body-div">
    <div class="main-div">
      <div class="nav-button-container"></div>
      @@BODY@@
    </div>
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