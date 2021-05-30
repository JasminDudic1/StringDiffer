const db = require('./index');

const generateDB = `
--Drop Table if exists Rating;


CREATE TABLE
Rating
(
ID SERIAL NOT NULL
, Rating INTEGER NOT NULL
, Diff INTEGER NOT NULL
,
CONSTRAINT Pk_Rating_ID PRIMARY KEY
(
ID
)
); `;

const tableExists = "SELECT * FROM Rating";

module.exports.createDB = async function createDB() {

     try {
        const exist = await db.pool.query(tableExists);
        if (exist) return;
    } catch (error) {
        console.log("Database doesnt Exists Already");
    }

    try {
        const res = await db.pool.query(generateDB);
        console.log("Generating a new database");
    } catch (error) {
        console.log("Error while generating\n ", error);
    }

}