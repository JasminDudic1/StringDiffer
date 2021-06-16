require("date-format-lite"); // add date format
 
class Storage {
    constructor(connection, table) {
        this._db = connection;
        this.table = "events";
    }
 
    // get events from the table, use dynamic loading if parameters sent
    async getAll(params,res) {

        try{
            let query = "SELECT * FROM events";
            let queryParams = [
                this.table
            ];

            let result=null;
     
            if (params.from && params.to) { 

                query += " WHERE end_date >= $1 AND start_date < $2";
                queryParams.push(params.from);
                queryParams.push(params.to);
                result =  await this._db.query(query,[params.from,params.to]);
            }else result  = await this._db.query(query);
        
           

            result.rows.forEach((entry) => {
                // format date and time
                entry.start_date = entry.start_date.format("YYYY-MM-DD hh:mm");
                entry.end_date = entry.end_date.format("YYYY-MM-DD hh:mm");
            });

            return result;
        }catch(err){
            console.log(err);
        }
    }
 
    // create new event
    async insert(data) {
            try{
            let result = await this._db.query(
                "INSERT INTO events (start_date, end_date, text) VALUES ($1,$2,$3)",
                [ data.start_date, data.end_date, data.text]);
                
            return {
                action: "inserted",
                tid: result.insertId
            }
        }catch(err){
            console.log(err);
        }
    }
 
    // update event
    async update(id, data) {
        try{
            await this._db.query(
                "UPDATE events SET start_date = $1, end_date = $2, text = $3 WHERE id = $4",
                [data.start_date, data.end_date, data.text, id]);
    
            return {
                action: "updated"
            }
        }
        catch(err){
            console.log(err);
        }

    }

    
 
    // delete event
    async delete(id) {
        try{
            await this._db.query(
                "DELETE FROM events WHERE id=$1;",
                [id]);
    
            return {
                action: "deleted"
            }
        }catch(err){
            console.log(err);
        }
    }
}
 
module.exports = Storage;