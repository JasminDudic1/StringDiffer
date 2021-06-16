const express = require('express');
const app = express();
const server = require('http').createServer(app);
const cors = require('cors');
var Sequelize = require('sequelize');

app.use(express.json());
app.use(cors());


const sequelize = new Sequelize('scheaduler', 'root', 'root', {
    host: 'localhost',
    dialect: 'mysql',
    define: {
      timestamps: false
    },
    pool: {
        max: 5,
        min: 0,
        acquire: 30000,
        idle: 10000
    }
  });

  const Event = sequelize.define('Event', {
    id: {
      type: Sequelize.STRING,
      primaryKey: true
    },
    start: {
        type: Sequelize.STRING(256)
      },
    end: {
        type: Sequelize.STRING(256)
      },
    person: {
        type: Sequelize.STRING(256)
      },
    title: {
        type: Sequelize.STRING(256)
      },
    content: {
        type: Sequelize.STRING(256)
      },
    color: {
        type: Sequelize.STRING(256)
      },
    actions: {
        type: Sequelize.STRING(512)
      },
    status: {
        type: Sequelize.STRING(256)
    },
    isClickable: {
        type: Sequelize.STRING(256)
    },
    isDisabled: {
        type: Sequelize.STRING(256)
    },
    draggable: {
        type: Sequelize.STRING(256)
    },
    resizable: {
        type: Sequelize.STRING(256)
    },
    data: {
      type: Sequelize.STRING(2256)
  }
  });

  sequelize.sync({force:false});

app.get('/', (req, res) => {
    res.send('<h1>Up and running.</h1>');
})

app.post("/add-new",(req,res) =>{

    const eventInfo = req.body.info;
    if(eventInfo==undefined){
        res.status(400);
        res.send({message:"Wrong json"});
        return;
    }
    try{
    Event.create({ 
      id:JSON.stringify(eventInfo.id),
      start:JSON.stringify(eventInfo.start),
      end:JSON.stringify(eventInfo.end),
      person:JSON.stringify(eventInfo.person),
      title:JSON.stringify(eventInfo.title),
      content:JSON.stringify(eventInfo.content),
      color:JSON.stringify(eventInfo.color),
      actions:JSON.stringify(eventInfo.actions),
      status:JSON.stringify(eventInfo.status),
      isClickable:JSON.stringify(eventInfo.isClickable),
      isDisabled:JSON.stringify(eventInfo.isDisabled),
      draggable:JSON.stringify(eventInfo.draggable),
      resizable:JSON.stringify(eventInfo.resizable),
      data:JSON.stringify(eventInfo)
    }).then(function (status) {
    });
    res.status(200);
    res.send({message:"Added"});
    }catch(e){
        console.log("Had error " + e);
        res.status(400);
    }
    

})

app.get("/get-all",(req,res) =>{


   // let events = Event.findAll();

    Event.findAll().then(function (events) {
        res.send(events);
    }); 

    /*console.log(events.every(event => event instanceof Event)); 
    res.send(all);*/

})

const PORT = process.env.PORT || 7777;
server.listen(PORT, () => console.log(`Listening on port ${PORT}`));