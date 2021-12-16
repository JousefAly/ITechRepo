// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//function Display() {
//    var x = document.getElementById("side_content");
//    if (x.style.visibility === "hidden") {
//        x.style.visibility = "visible";
//    } else {
//        x.style.visibility = "hidden";
//    }
       
//}

function openNav() {
    document.getElementById("mySidebar").style.width = "250px";
    document.getElementById("main2").style.marginLeft = "40px";
    document.getElementById("main3").style.marginLeft = "100px";

}

function closeNav() {
    document.getElementById("mySidebar").style.width = "0";
    document.getElementById("main2").style.marginLeft = "0";
    document.getElementById("main3").style.marginLeft= "0";
}