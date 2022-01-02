// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


/* start of side-bar Javascript code */

function openNav() {
    document.getElementById("mySidebar").style.width = "250px";
    document.getElementById("main1").style.marginLeft = "100px";
    document.getElementById("main2").style.marginLeft = "40px";
    document.getElementById("main3").style.marginLeft = "100px";
    document.getElementById("openptn").style.zIndex = "0";

}

function closeNav() {
    document.getElementById("mySidebar").style.width = "0";
    document.getElementById("main1").style.marginLeft = "0";
    document.getElementById("main2").style.marginLeft = "0";
    document.getElementById("main3").style.marginLeft = "0";
    document.getElementById("openptn").style.zIndex = "1";
}

/* end of side-bar Javascript code  */