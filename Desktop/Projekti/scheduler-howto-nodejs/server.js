const express = require("express"); // use Express
const bodyParser = require("body-parser"); // for parsing POST requests
const app = express(); // create application
const port = process.env.PORT || 3000;
const db = require("./db/conn.js");
const router = require("./router");
const cors = require('cors');

const DBStartHelper = require('./db/start.js');

DBStartHelper.createDB();
 
// It's necessary for parsing POST requests
// the line below is used for parsing application/x-www-form-urlencoded
app.use(bodyParser.urlencoded({extended:true}));
app.use(cors());
app.use(express.static(__dirname + "/public"));


const Storage = require("./storage");
const storage = new Storage(db.pool);
router.setRoutes(app, "/data", storage);
 
// start server
app.listen(port, () => {
    console.log("Server is running on port " + port + "...");
});