// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Exercise 1
// let link1 = document.getElementById('a1');
// let isClicked = false;

// link1.addEventListener('click', function () {
//     if (isClicked) {
//         link1.style.backgroundColor = '';
//     } else {
//         link1.style.backgroundColor = 'red';
//     }

//     isClicked = !isClicked;

//     return false;
// });

// Exercise 2
// let link2 = document.getElementById('a2');
// let paragraph = link2.parentNode.querySelector('p');
// let isClicked2 = false;

// link2.addEventListener('click', function () {
//     if (isClicked2) {
//         paragraph.style.color = '';
//         paragraph.style.fontWeight = '';
//     } else {
//         paragraph.style.color = 'blue';
//         paragraph.style.fontWeight = 'bold';
//     }

//     isClicked2 = !isClicked2;

//     return false;
// });

// Exercise 3
// var link3 = document.getElementById('a3');
// var paragraph3 = link3.parentNode.querySelector('p');
// var isClicked3 = false;
// var originalParagraphContent = paragraph3.innerHTML;

// link3.addEventListener('click', function () {
//     if (isClicked3) {
//         paragraph3.innerHTML = originalParagraphContent;
//     } else {
//         paragraph3.innerHTML = '';
//     }

//     isClicked3 = !isClicked3;
// });

// get details of a Pokemon
function clickDetailPokemon(url) {
    fetch(url)
        .then(response => response.json())
        .then(data => {
            let modalTitle = document.querySelector(".modal-title");
            let img1 = document.getElementById("img1");
            let img2 = document.getElementById("img2");
            let img3 = document.getElementById("img3");
            let img4 = document.getElementById("img4");
            let height = document.querySelector("#product-desc p:nth-child(1)");
            let weight = document.querySelector("#product-desc p:nth-child(2)");
            let baseExperience = document.querySelector("#product-desc p:nth-child(3)");
            let abilities = document.querySelector("#product-desc p:nth-child(4)");
            let types = document.querySelector("#product-desc p:nth-child(5)");

            modalTitle.innerText = data.name;
            img1.src = data.sprites.front_default;
            img1.alt = data.name;
            img2.src = data.sprites.front_default;
            img3.src = data.sprites.back_default;
            img4.src = data.sprites.front_shiny;
            height.innerText = "Height: " + data.height;
            weight.innerText = "Weight: " + data.weight;
            baseExperience.innerText = "Base Experience: " + data.base_experience;
            abilities.innerText = "Abilities: " + data.abilities.map(ability => ability.ability.name).join(", ");
            types.innerText = "Types: " + data.types.map(type => type.type.name).join(", ");

            $('#modal-lg').modal('show');
        })
        .catch(error => console.error('Unable to get Pokemon details.', error));
}

// get all employees from API
$(document).ready(function () {
    $('#myTable2').DataTable({
        ajax: {
            url: "https://localhost:7000/api/employees",
            dataType: "json",
            dataSrc: "data"
        },
        dom: 'Bfrtip',
        buttons: [
            'colvis', 'copy',
            {
                extend: 'excelHtml5',
                title: 'Excel',
                text: 'Export to excel',
                className: "btn-primary",
                //Columns to export
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7]
                }
            },
            {
                extend: 'pdfHtml5',
                title: 'PDF',
                text: 'Export to PDF',
                //Columns to export
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7]
                }
            },
            {
                extend: 'print',
                exportOptions: {
                    columns: ':visible'
                }
            }
        ],
        autoWidth: true,
        columns: [
            {
                data: 'no',
                render: function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            { data: "nik" },
            {
                data: 'fullName',
                render: function (data, type, row) {
                    return row.firstName + ' ' + row.lastName;
                }
            },
            {
                //data: "birthDate"
                data: "birthDate",
                render: function (data, type, row) {
                    return moment(data).format("DD MMMM YYYY");
                }
            },
            {
                data: "gender",
                render: function (data, type, row) {
                    if (data === 0)
                        return "Female";
                    else
                        return "Male";
                }
            },
            {
                //data: "hiringDate"
                data: "hiringDate",
                render: function (data, type, row) {
                    return moment(data).format("DD MMMM YYYY");
                }
            },
            { data: "email" },
            { data: "phoneNumber" }
        ]
    });
});
