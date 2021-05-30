
//const url = 'http://109.237.47.100:3000';
const url = 'https://jdudic1stringdiff.herokuapp.com';

let diffindex=1;

let chartVisible = false;


function refresh(diff){

    
   // diff = document.getElementById("diffs"); 

   

    if(diff==="jsdiff"){
        document.getElementById("jsdiffdiv").style.display = "block";
        document.getElementById("jsdifflibdiv").style.display = "none";
        document.getElementById("patiencediv").style.display = "none";
        document.getElementById("textdiv").style.display = "none";
        diffindex=1;
    }else if(diff==="jsdifflib"){
        document.getElementById("jsdiffdiv").style.display = "none";
        document.getElementById("jsdifflibdiv").style.display = "block";
        document.getElementById("patiencediv").style.display = "none";
        document.getElementById("textdiv").style.display = "none";
        diffindex=2;
    }else if(diff==="patience"){
        document.getElementById("jsdiffdiv").style.display = "none";
        document.getElementById("jsdifflibdiv").style.display = "none";
        document.getElementById("patiencediv").style.display = "block";
        document.getElementById("textdiv").style.display = "none";
        diffindex=3;
    }else if(diff==="text"){
        document.getElementById("jsdiffdiv").style.display = "none";
        document.getElementById("jsdifflibdiv").style.display = "none";
        document.getElementById("patiencediv").style.display = "none";
        document.getElementById("textdiv").style.display = "block";
        diffindex=4;
    }else return;
    submit();

 


}

async function setChart(){

  document.getElementById("chartBtn").style.visibility='visible';
  if(!chartVisible)return;

  console.log("Reseting chart");


  fetch(url+"/rate/get/"+diffindex)
  .then(response => response.json())
  .then(data =>{

  
    // set the data
 /* var data = [
    {x: "White", value: 223553265},
    {x: "Black or African American", value: 38929319},
    {x: "American Indian and Alaska Native", value: 2932248},
    {x: "Asian", value: 14674252},
    {x: "Native Hawaiian and Other Pacific Islander", value: 540013},
    {x: "Some Other Race", value: 19107368},
    {x: "Two or More Races", value: 9009073}
];*/
  var chartData = [];
  console.log(data);
  for(let i = 1 ; i<data.length;i++){
    chartData.push({x:(i+1), value:data[i]});
  }



  // create the chart
  var chart = anychart.pie();

  // set the chart title
  chart.title("PieChart of anonymus ratings of the current diff.");

  // add the data
  chart.data(data);

  // display the chart in the container
  document.getElementById("chartDiv").style.display = "block";
  document.getElementById("chartDiv").innerHTML = '';
  chart.container('chartDiv');
  chart.draw();
  
  
  });

}

function showChart(){

chartVisible=!chartVisible;


if(chartVisible) setChart();
else document.getElementById("chartDiv").style.display = "none";

}

function submit(){


  resetRating();
  setChart();
  if(diffindex==1){

  check();
  
  }else if(diffindex==2){

    if(document.getElementById("sidebyside").checked )diffUsingJS(0);
    else if(document.getElementById("inline").checked )diffUsingJS(1);

      
  }else if(diffindex==3){

    
      if(document.getElementById("patienceminus").checked ){
          calcDiff(true);
      }
      else if(document.getElementById("patienceplus").checked ){
          calcDiff(false);
      }

  }else if(diffindex==4){
   
  var dmp = new diff_match_patch();

  var text1 = document.getElementById('baseText').value;
  var text2 = document.getElementById('newText').value;
  dmp.Diff_Timeout = parseFloat(document.getElementById('timeout').value);
  dmp.Diff_EditCost = parseFloat(document.getElementById('editcost').value);

  var ms_start = (new Date()).getTime();
  var d = dmp.diff_main(text1, text2);
  var ms_end = (new Date()).getTime();

  if (document.getElementById('semantic').checked) {
    dmp.diff_cleanupSemantic(d);
  }
  if (document.getElementById('efficiency').checked) {
    dmp.diff_cleanupEfficiency(d);
  }
  var ds = dmp.diff_prettyHtml(d);
  document.getElementById('result').innerHTML = ds;

 

  }
  else {
    resetDivs();
    return;
  }

}

function resetRating(){
  let rating = document.getElementById("rating");
  rating.style.visibility='visible';

  const ratingLoad = "diff"+diffindex;

  const ratingExist = localStorage.getItem("diff"+diffindex);

  if(ratingExist==null){
    document.getElementById("star5").checked =document.getElementById("star4").checked=document.getElementById("star3").checked = document.getElementById("star2").checked = document.getElementById("star1").checked=false;
  }else{
    document.getElementById("star"+ratingExist).checked=true;
  }

}

function startup(){

  console.log("Reseting all");
  localStorage.clear();
  resetDivs();
  
}

function resetDivs(){
  document.getElementById("result").innerHTML="";;
  document.getElementById("rating").style.visibility='hidden';
  document.getElementById("chartBtn").style.visibility='hidden';

}

async function rated(number){

  const ratingExist = localStorage.getItem("diff"+diffindex);

  if(ratingExist == null){

  const response = await fetch(url+"/rate/add", {
    method: 'POST', // *GET, POST, PUT, DELETE, etc.
    headers: {
      'Content-Type': 'application/json'
      // 'Content-Type': 'application/x-www-form-urlencoded',
    },
    body: JSON.stringify({rating:number,diff:diffindex})
  }).then(()=>{

  const ratingSave = "diff"+diffindex;
  localStorage.setItem(ratingSave,number);
  setChart();

  });

}else{

  const response = await fetch(url+"/rate/update", {
    method: 'POST', // *GET, POST, PUT, DELETE, etc.
    headers: {
      'Content-Type': 'application/json'
      // 'Content-Type': 'application/x-www-form-urlencoded',
    },
    body: JSON.stringify({newRating:number,oldRating:ratingExist,diff:diffindex})
  }).then(()=>{

  const ratingSave = "diff"+diffindex;
  localStorage.setItem(ratingSave,number);
  setChart();

  });


}

  

}

function calcDiff(diffVsPlus) {


    var a = document.getElementById("baseText").value.split("\n");
    var b = document.getElementById("newText").value.split("\n");
    if (diffVsPlus) {
      var diff = patienceDiff(a , b);
    } else {
      var diff = patienceDiffPlus(a , b);
    }
    
    var diffLines = "";
    diff.lines.forEach((o) => {
      if (o.bIndex < 0 && o.moved) {
        diffLines += "-m  ";
      } else if (o.moved) {
        diffLines += "+m  ";
      } else if (o.aIndex < 0) {
        diffLines += "+   ";
      } else if (o.bIndex < 0) {
        diffLines += "-   ";
      } else {
        diffLines += "    ";
      }
      diffLines += o.line + "\n";
    });

    let x = document.createElement("TEXTAREA");
    x.value = diffLines;
    
   let result = document.getElementById("result");
   result.innerHTML="";
   result.appendChild(x);

  }