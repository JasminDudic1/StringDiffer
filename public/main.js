
let diffindex=1;

function refresh(diff){

    
   // diff = document.getElementById("diffs"); 

   

    if(diff==="jsdiff"){
        document.getElementById("jsdiffdiv").style.display = "block";
        document.getElementById("jsdifflibdiv").style.display = "none";
        document.getElementById("patiencediv").style.display = "none";
        document.getElementById("textdiv").style.display = "none";
        diffindex=1;
        submit();
    }else if(diff==="jsdifflib"){
        document.getElementById("jsdiffdiv").style.display = "none";
        document.getElementById("jsdifflibdiv").style.display = "block";
        document.getElementById("patiencediv").style.display = "none";
        document.getElementById("textdiv").style.display = "none";
        diffindex=2;
        submit();
    }else if(diff==="patience"){
        document.getElementById("jsdiffdiv").style.display = "none";
        document.getElementById("jsdifflibdiv").style.display = "none";
        document.getElementById("patiencediv").style.display = "block";
        document.getElementById("textdiv").style.display = "none";
        diffindex=3;
        submit();
    }else if(diff==="text"){
        document.getElementById("jsdiffdiv").style.display = "none";
        document.getElementById("jsdifflibdiv").style.display = "none";
        document.getElementById("patiencediv").style.display = "none";
        document.getElementById("textdiv").style.display = "block";
        diffindex=4;
        submit();
    }

 


}


function submit(){


  resetRating();
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
  }



}

function resetRating(){
  let rating = document.getElementById("rating");
  rating.style.visibility='visible';
}

function startup(){


  resetDivs();
  
}

function resetDivs(){
  let result = document.getElementById("result");
  result.innerHTML="";
let rating = document.getElementById("rating");
rating.style.visibility='hidden';
}

function rated(number){

  


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