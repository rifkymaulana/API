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
            {data: "nik"},
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
            {data: "email"},
            {data: "phoneNumber"},
            {
                data: 'action',
                render: function (data, type, row) {
                    return `<div><button onclick="Get('${row.guid}')" data-toggle="modal" data-target="#modal-xl-update" class="btn btn-warning">Update</button> <button onclick="Delete('${row.guid}')" class="btn btn-danger">Delete</button></div>`;

                }
            }
        ]
    });
});

function createEmployee() {
    let nik = document.querySelector("#nik").value;
    let firstName = document.querySelector("#firstName").value;
    let lastName = document.querySelector("#lastName").value;
    let birthDate = document.querySelector("#birthDate").value;
    let gender = document.querySelector("#gender").value;
    let genderEnum;
    if (gender === "0") {
        genderEnum = 0;
    } else if (gender === "1") {
        genderEnum = 1;
    }
    let hiringDate = document.querySelector("#hiringDate").value;
    let email = document.querySelector("#email").value;
    let phoneNumber = document.querySelector("#phoneNumber").value;
    // Data ke api
    let data = {
        nik: nik,
        firstName: firstName,
        lastName: lastName,
        birthDate: birthDate,
        gender: genderEnum,
        hiringDate: hiringDate,
        email: email,
        phoneNumber: phoneNumber
    };

    console.log(data);

    $.ajax({
        url: "https://localhost:7000/api/employees", // Sesuaikan URL sesuai dengan endpoint API Anda
        type: "POST",
        data: JSON.stringify(data),
        contentType: "application/json"
    }).done(result => {
        alert("Insert successful"); // Tampilkan alert pemberitahuan jika berhasil
        location.reload();
    }).fail(error => {
        alert("Insert failed"); // Tampilkan alert pemberitahuan jika gagal
    });
}

function Get(data) {
    console.log(data);
    $.ajax({
        url: "https://localhost:7000/api/employees/" + data, // Sesuaikan URL sesuai dengan endpoint API Anda
        type: "GET",
        dataType: "json"
    }).done(res => {
        $("#guidU").val(res.data.guid);
        $("#nikU").val(res.data.nik);
        $("#firstNameU").val(res.data.firstName);
        $("#lastNameU").val(res.data.lastName);
        let birthDate = moment(res.data.birthDate).format('yyyy-MM-DD');
        $("#birthDateU").val(birthDate);
        let genderEnum = res.data.gender;
        if (genderEnum === 0) {
            gender = "0";
        } else if (genderEnum === 1) {
            gender = "1";
        }
        $("#genderU").val(gender);
        let hiringDate = moment(res.data.hiringDate).format('yyyy-MM-DD');
        $("#hiringDateU").val(hiringDate);
        $("#emailU").val(res.data.email);
        $("#phoneNumberU").val(res.data.phoneNumber);
        console.log(res);
    }).fail(error => {
        alert("Insert failed"); // Tampilkan alert pemberitahuan jika gagal
    });
}

function Delete(deleteId) {
    Swal.fire({
        title: 'Konfirmasi',
        text: 'Apakah Anda yakin ingin menghapus data ini?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Ya, Hapus',
        cancelButtonText: 'Batal'
    }).then((result) => {
        if (result.isConfirmed) {
            // Jika pengguna mengonfirmasi untuk menghapus
            $.ajax({
                url: "https://localhost:7000/api/employees?guid=" + deleteId, // Sesuaikan URL sesuai dengan endpoint API Anda
                type: "DELETE",
            }).done(result => {
                Swal.fire('Deleted!', 'Data berhasil dihapus.', 'success').then(() => {
                    location.reload();
                });
            }).fail(error => {
                Swal.fire('Error!', 'Gagal menghapus data.', 'error');
            });
        }
    });
}

function Update() {
    // Mendapatkan nilai-nilai input
    let firstName = document.querySelector("#firstNameU").value;
    let guid = document.querySelector("#guidU").value;
    let nik = document.querySelector("#nikU").value;
    let lastName = document.querySelector("#lastNameU").value;
    let birthDate = document.querySelector("#birthDateU").value;
    let gender = document.querySelector("#genderU").value;
    let genderEnum;
    if (gender === "0") {
        genderEnum = 0;
    } else if (gender === "1") {
        genderEnum = 1;
    }
    let hiringDate = document.querySelector("#hiringDateU").value;
    let email = document.querySelector("#emailU").value;
    let phoneNumber = document.querySelector("#phoneNumberU").value;
    // Data ke api
    let data = {
        firstName: firstName,
        lastName: lastName,
        birthDate: birthDate,
        gender: genderEnum,
        hiringDate: hiringDate,
        email: email,
        phoneNumber: phoneNumber,
        nik: nik,
        guid: guid
    };
    console.log(data);
    $.ajax({
        url: "https://localhost:7000/api/employees", // Sesuaikan URL sesuai dengan endpoint API Anda
        type: "PUT",
        data: JSON.stringify(data),
        contentType: "application/json"
    }).done(result => {
        Swal.fire({
            position: 'center',
            icon: 'success',
            title: 'Your work has been saved',
            showConfirmButton: true,
            confirmButtonText: 'OK',
        }).then((result) => {
            if (result.isConfirmed) {
                location.reload();
            }
        });
    }).fail(error => {
        alert("Insert failed"); // Tampilkan alert pemberitahuan jika gagal
    });
}


// create chart
var ctx = document.getElementById('myChart').getContext('2d');
var myChart = new Chart(ctx, {
    type: 'bar',
    responsive: true,
    data: {
        labels: ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
        datasets: [{
            label: '# of Votes', // label
            data: [12, 19, 3, 5, 2, 3], // data
            backgroundColor: [ // warna
                'rgba(255, 99, 132, 0.2)', // red
                'rgba(54, 162, 235, 0.2)', // blue
                'rgba(255, 206, 86, 0.2)', // yellow
                'rgba(75, 192, 192, 0.2)', // green
                'rgba(153, 102, 255, 0.2)', // purple
                'rgba(255, 159, 64, 0.2)' // orange
            ],
            borderColor: [ // warna border
                'rgba(255, 99, 132, 1)', // red
                'rgba(54, 162, 235, 1)', // blue
                'rgba(255, 206, 86, 1)', // yellow
                'rgba(75, 192, 192, 1)', // green
                'rgba(153, 102, 255, 1)', // purple
                'rgba(255, 159, 64, 1)' // orange
            ],
            borderWidth: 1 // ketebalan border
        }]
    },
    options: {
        scales: {
            y: {
                beginAtZero: true // mulai dari 0
            }
        }
    }
});
