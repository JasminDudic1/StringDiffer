let max = 1;

async function getEvents(scheduler){

    fetch("/data")
  .then(response => response.json())
  .then(data =>{

      parseJSON = [];

    for(let i = 0 ; i <data.rowCount ; i ++){
      if(data.rows[i].id >max )max = data.rows[i].id +1;
        parseJSON.push({
            id:data.rows[i].id,
            start_date :data.rows[i].start_date,
            end_date: data.rows[i].end_date,
            text: data.rows[i].text
        });

    }

    scheduler.parse(parseJSON);

      return parseJSON;

  });

}