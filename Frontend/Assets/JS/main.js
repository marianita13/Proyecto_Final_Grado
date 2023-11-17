const sign_in_btn = document.querySelector("#sign-in-btn");
const sign_up_btn = document.querySelector("#sign-up-btn");
const container = document.querySelector(".container");

sign_up_btn.addEventListener('click', () =>{
    container.classList.add("sign-up-mode");
});

sign_in_btn.addEventListener('click', () =>{
    container.classList.remove("sign-up-mode");
});


/* LOGIN */

const userlogin = document.querySelector("#user-login");
const passwordlogin = document.querySelector("#password-login");


/* SIGN UP */
const user = document.querySelector("#user");
const email = document.querySelector("#email");
const password = document.querySelector("#password");
const buttonUp = document.querySelector("#Sign-Up")

// Validate Login Form
buttonUp.addEventListener('click',(evento) =>{
    evento.preventDefault();
    var loginUser = user.value;
    var emailUser = email.value;
    var loginPass = password.value;
    if (loginUser == "" || loginPass == "") {
        alert("Please Fill All Fields!");
        return false;
    }
    else{
        data = {
            "username": loginUser,
            "password": loginPass,
            "email": emailUser
        }
        var Json = JSON.stringify(data);
        localStorage.setItem("Usuario",Json);
        var StoredData = localStorage.getItem("Usuario");
        var usario = JSON.parse(StoredData);
        console.log(usario);
        location.assign("../Index.html")
    }
})

const Consult = document.querySelector("#employees");

Consult.addEventListener('click', (evento) => {
    localStorage.clear();
    var prueba = "AAAAAA"
    localStorage.setItem("prueba",prueba)
})