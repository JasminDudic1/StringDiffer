
let currentStep = 1;
let randomStructures = [];

//main method for registering button click
//depenting on the current step it will either call the
//1) Generate method that will generate 4-14 random structures
//2) Generate the map used for calculation
//3) Confirm reset step, will trigger confirm on button
//4) reset step will clear all variables and reset text

function buttonClick(){

    switch(currentStep){
        case 1:
            generateStructures();
            currentStep++;
            break;
        case 2:
            generateMaps(randomStructures);
            currentStep++;
            break;
        case 3:
            document.getElementById("mainBtn").textContent = "Confirm Reset";
            currentStep++;
            break;
        case 4:
            reset();
             break;
    }

}

function reset(){
    currentStep=1;
    randomStructures=[];
    document.getElementById("random").innerHTML = " ";
    document.getElementById("maps").innerHTML = " ";
    document.getElementById("answer").innerHTML = " ";
    document.getElementById("mainBtn").textContent = "Generate Random";
}

//method that generates random structures
function generateStructures(){

    //generates random number between 4 and 10
    let randomAmount = Math.floor(Math.random() * 10) + 4; 

    //max number used as offset when generating id's
    let maxId =0 ;
    

    //i just like fori loops way more than any other ones
    for(let i = 0 ; i <randomAmount ; i++ ){

        //generates a random number that is at most 2 higher than the highest number
        //this way it will have some empty id's in between
        //so that the optimal solution wont always as easy as id = max +1;
        let randomId = Math.floor(Math.random() * 2) + 1+maxId;
        maxId = randomId;
        //generates a random value between 0 and 10
        let randomValue = Math.floor(Math.random() * 10)+1;

        randomStructures.push({id:randomId,value:randomValue});

    }

    //after generating values
    //i will force one value to be doubled by taking two different random id's
    //and giving them the same value

    let firstId =  Math.floor(Math.random() * (randomAmount-1)) ;
    let secondId = firstId;

    while(firstId === secondId){
     secondId =  Math.floor(Math.random() * (randomAmount-1)) ;

    }

    
    randomStructures[firstId].value = randomStructures[secondId].value;

    if(document.getElementById("shuffleBox").checked)
    shuffle(randomStructures);

    document.getElementById("random").innerHTML = JSON.stringify(randomStructures);
    document.getElementById("mainBtn").textContent = "Generate Answer";

    

}

//method to shuffle up all id's
//so it is not in an ascending order
// https://stackoverflow.com/questions/2450954/how-to-randomize-shuffle-a-javascript-array
function shuffle(array) {
    var currentIndex = array.length,  randomIndex;

    while (0 !== currentIndex) {

      randomIndex = Math.floor(Math.random() * currentIndex);
      currentIndex--;
  

      [array[currentIndex], array[randomIndex]] = [
        array[randomIndex], array[currentIndex]];
    }
  
    return array;
  }





//method that takes the random structures and generates some usefull maps
//that can be used to speedup defining new unique values
function generateMaps(structuresArray){

    let idMaps=[];
    let valuesMap = [];

    for(let i = 0 ; i <structuresArray.length ; i++){

        //set that id is used in map
        idMaps[structuresArray[i].id]=true;

        //if map is empty at that location
        //fill it
        if(valuesMap[structuresArray[i].value]==null)
            valuesMap[structuresArray[i].value]=1;
        else valuesMap[structuresArray[i].value]++;


    }


    document.getElementById("maps").innerHTML = JSON.stringify(idMaps) + "<br> "+ JSON.stringify(valuesMap);

    //sets starting unique id to something that can not be used
    let uniqueId = -1;

    //checks if there is a null in the map
    //if it is that means that that id is not used
    //so take that id and break
    for( let i = 1 ; i < idMaps.length; i++){
        if(idMaps[i]==null){
            uniqueId = i;
            break;
        }
    }

    //if id did not change that means that there are no null openings
    //so the new id is max+1
    //this could have been used immediately to get the uniqueid
    //but since i need the maps for values it doesnt take that much more time 
    ///to check for the id this way
    if(uniqueId==-1)uniqueId = idMaps.length+1;

    //i will use on value for both finding the value that occurs twice
    //and finding the next positive integer value that can be used
    let doubleValue = -1;
    let newValue = -1;
    for (let i = 1 ; i < valuesMap.length; i++){

        //if the value occured twice i remember it
        if(valuesMap[i]>=2)doubleValue=i;

        //then if the next value is null, meaning it is not being used and is next positive integer
        //then i remember that and break
        else if (valuesMap[i]==null && doubleValue!=-1){
            newValue = i ;
            break;
        }

    }

    //this should never happen
    //but just in case
    if(doubleValue == -1){
        document.getElementById("answer").innerHTML = "No value occured twice";
        document.getElementById("mainBtn").textContent = "Reset";
        return;
    }

    //once more if all values are in ascending order without any empty slots
    //then it will not find it and i will have to take maxValue +1;
    if(newValue==-1)newValue = valuesMap.length;

    document.getElementById("answer").innerHTML = JSON.stringify({id:uniqueId,value:newValue});
    document.getElementById("mainBtn").textContent = "Reset";

}

