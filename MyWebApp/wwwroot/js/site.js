// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Exercise 1
let link1 = document.getElementById('a1');
let isClicked = false;

link1.addEventListener('click', function () {
    if (isClicked) {
        link1.style.backgroundColor = '';
    } else {
        link1.style.backgroundColor = 'red';
    }

    isClicked = !isClicked;

    return false;
});

// Exercise 2
let link2 = document.getElementById('a2');
let paragraph = link2.parentNode.querySelector('p');
let isClicked2 = false;

link2.addEventListener('click', function () {
    if (isClicked2) {
        paragraph.style.color = '';
        paragraph.style.fontWeight = '';
    } else {
        paragraph.style.color = 'blue';
        paragraph.style.fontWeight = 'bold';
    }

    isClicked2 = !isClicked2;

    return false;
});

// Exercise 3
var link3 = document.getElementById('a3');
var paragraph3 = link3.parentNode.querySelector('p');
var isClicked3 = false;
var originalParagraphContent = paragraph3.innerHTML;

link3.addEventListener('click', function() {
    if (isClicked3) {
        paragraph3.innerHTML = originalParagraphContent;
    } else {
        paragraph3.innerHTML = '';
    }

    isClicked3 = !isClicked3;
});

