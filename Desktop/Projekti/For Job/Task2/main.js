
//some random example for texting it on multiple levels
    //this json is terribly formatted but its what i came up with
const jsonObject = {

    "Person":{
        "Name":"Jasmin",
        "Surname":"Dudic",
        "Location":{
            "City":{
                "Name":"Sarajevo",
                "Country":{
                    "Name":"BiH",
                    "Continent":"Europe"
                }
            }
        }
    }
}

//gets called at startup and will show the defined object as a text on screen
//for easier testing purposes
function startup(){

    document.getElementById("jsonDiv").innerHTML = " <pre> "+JSON.stringify(jsonObject,null," ")+" </pre>";
}


function lookup(){

   //split the input path
   let pathJson = document.getElementById("pathInput").value.split('.');

   //some varaible to move through the object
   let currentJSON = jsonObject;
   let currentPath = "";

   for(let i = 0; i <pathJson.length; i++){
    currentPath = currentPath + "."+pathJson[i];
    //if it cant move forward throw error on screen
     if(currentJSON[pathJson[i]]==null){
        throwError(currentPath);
        return;
     }  
     //else move forward
     currentJSON=currentJSON[pathJson[i]];
   
     

   }

   //at the end show the result
   document.getElementById("result").innerHTML= "Value of the full path is <br>" + JSON.stringify(currentJSON);

}

function throwError(path){
    //throw error on screen
    path = path.substring(1);
    document.getElementById("result").innerHTML= "Could not find value :<br>"+path;

}

