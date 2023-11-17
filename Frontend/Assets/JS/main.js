const sign_in_btn = document.querySelector("#sign-in-btn");
const sign_up_btn = document.querySelector("#sign-up-btn");
const container = document.querySelector(".container");

sign_up_btn.addEventListener('click', () =>{
    container.classList.add("sign-up-mode");
});

sign_in_btn.addEventListener('click', () =>{
    container.classList.remove("sign-up-mode");
});



function crearInfo(fileName, data) {
    const filePath = `../../Assets/${fileName}`;
    const fileData = JSON.parse(readFileSync(filePath));
    fileData.data.push(data);
    writeFileSync(filePath, JSON.stringify(fileData, null, 4));

}


function checkfile(filePath) {
    try {
        fs.readFileSync(filePath);
        return true;
    } catch (error) {
        if (error.code === 'ENOENT') {
        console.error("El archivo no existe.");
        } else {
        console.error("Ha ocurrido un error al leer el archivo.");
        }
        return false;
    }
}


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
    var diccUser = {}
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
        diccUser.data = diccUser
        crearInfo("user.json",data)
        console.log(data);
    }
})
