function startup(){

    scheduler.config.first_hour = 6;
    scheduler.config.last_hour = 19;
    scheduler.init('scheduler_here', new Date().now, "week");
    scheduler.parse([
    /*    { id:1, start_date: "2019-04-15 09:00", end_date: "2019-04-15 12:00", text:"English lesson" },
        { id:2, start_date: "2019-04-16 10:00", end_date: "2019-04-16 16:00", text:"Math exam" },
        { id:3, start_date: "2019-04-16 10:00", end_date: "2019-04-21 16:00", text:"Science lesson" },
        { id:4, start_date: "2019-04-17 16:00", end_date: "2019-04-17 17:00", text:"English lesson" },
        { id:5, start_date: "2019-04-18 09:00", end_date: "2019-04-18 17:00", text:"Usual event" }*/
    ]); 


}


function callEvent(text, array){

    console.log("Is Called");

}