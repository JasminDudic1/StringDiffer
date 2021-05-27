const express = require('express');
const app = express();
const server = require('http').createServer(app);
const cors = require('cors');
const fs = require('fs');


app.use(express.json());
app.use(cors());


app.use(express.static("public"));

const port = process.env.PORT || 8000;

server.listen(port, () => console.log(`Listening on port ${port}`));