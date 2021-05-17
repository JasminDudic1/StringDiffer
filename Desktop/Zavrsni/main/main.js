
let diffindex=1;

function refresh(diff){

    
   // diff = document.getElementById("diffs"); 

    if(diff.value==="jsdiff"){
        document.getElementById("jsdiffdiv").style.display = "block";
        document.getElementById("jsdifflibdiv").style.display = "none";
        document.getElementById("patiencediv").style.display = "none";
        document.getElementById("textdiv").style.display = "none";
        diffindex=1;
    }else if(diff.value==="jsdifflib"){
        document.getElementById("jsdiffdiv").style.display = "none";
        document.getElementById("jsdifflibdiv").style.display = "block";
        document.getElementById("patiencediv").style.display = "none";
        document.getElementById("textdiv").style.display = "none";
        diffindex=2;
    }else if(diff.value==="patience"){
        document.getElementById("jsdiffdiv").style.display = "none";
        document.getElementById("jsdifflibdiv").style.display = "none";
        document.getElementById("patiencediv").style.display = "block";
        document.getElementById("textdiv").style.display = "none";
        diffindex=3;
    }else if(diff.value==="text"){
        document.getElementById("jsdiffdiv").style.display = "none";
        document.getElementById("jsdifflibdiv").style.display = "none";
        document.getElementById("patiencediv").style.display = "none";
        document.getElementById("textdiv").style.display = "block";
        diffindex=4;
    }

   let result = document.getElementById("result");
   result.innerHTML="";


}


function submit(){


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

}else console.log("What");



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