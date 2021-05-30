const express = require('express');
const app = express();
const server = require('http').createServer(app);
const cors = require('cors');
const fs = require('fs');


const db = require('./db/index');

const DBStartHelper = require('./db/start');

DBStartHelper.createDB();

app.use(express.json());
app.use(cors());


app.use(express.static("public"));

app.post('/rate/update', function (req, res) {


    const{newRating,oldRating,diff} = req.body;
    if(newRating == null || oldRating == null ||diff == null){
        console.log(JSON.stringify(req.body));
        res.status(500);
        res.send("Got wrong json");
        return;
    }

    try{
    const insert = "UPDATE rating set rating=$1 WHERE ID =(SELECT ID FROM rating WHERE rating = $2 and diff = $3 ORDER BY ID LIMIT 1) ;"

    const result = db.pool.query(insert,[newRating,oldRating,diff])
    res.status(200);
    res.send();
    return;
    }catch(err){
        res.status(300);
        res.send({error:err});
        return;
    }

  });


app.post('/rate/add', function (req, res) {


    const{rating,diff} = req.body;
    if(rating == null ||diff == null){
        console.log(JSON.stringify(req.body));
        res.status(500);
        res.send("Got wrong json");
        return;
    }

    try{
    const insert = ("Insert into Rating(Rating, Diff) values ($1,$2)");

    const result = db.pool.query(insert,[rating,diff])
    res.status(200);
    res.send();
    return;
    }catch(err){
        res.status(300);
        res.send({error:err});
        return;
    }

  });


app.get('/rate/get/:diff', async function(req,res){

    const diff = req.params.diff;

    if(diff == null){
        res.status(400);
        res.send("Got wrong json");
        return;
    }

    returnJson = [0];

    try{

       // const select = "select rating from rating where diff=$1;"

       for(let i = 1 ; i < 6 ; i ++){

        const select = "select count(rating) from rating where diff=$1 and rating = $2;";

        const selectRes =await db.pool.query(select , [diff,i]);

        returnJson.push(selectRes.rows[0].count);

       }

        res.status(200);
        res.send(returnJson);


    }catch(err){
        res.status(500);
        res.send({error:err});
        return;
    }


});

app.get('/reset',async function(req,res){

    try{

        const reset = "delete from rating where 1 = 1";

        const resetRes = await db.pool.query(reset);
        res.status(200);
        res.send({success:true});


    }catch(err){
        res.status(300);
        res.send({error:err});
        return;
    }


});

const port = process.env.PORT || 3000;

server.listen(port, () => console.log(`Listening on port ${port}`));