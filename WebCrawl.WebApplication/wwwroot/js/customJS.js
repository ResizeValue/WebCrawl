const urlSearchBtn = document.getElementById('parseBtn');

if(urlSearchBtn){
    // $(urlSearchBtn).click(function (e) { 
    //     e.preventDefault();
    //     console.log("preventDefault");
    //     sendParseRequest();
    // });
}

function sendParseRequest(){
    urlSearchBtn.disabled = true;
    document.getElementById("url").disabled = true;

    $(urlSearchBtn).val("Parsing...");

    console.log("Parsing");

    urlSearchBtn.addEventListener("submit", true);
}