// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function hideItem() {

    document.getElementById("element").style.opacity = 0;
    document.getElementById("element").style.transition = "all 2s";
}

setTimeout(hideItem, 4000)