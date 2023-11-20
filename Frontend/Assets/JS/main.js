const sign_in_btn = document.querySelector("#sign-in-btn");
const sign_up_btn = document.querySelector("#sign-up-btn");
const container = document.querySelector(".container");

sign_up_btn.addEventListener('click', () =>{
    container.classList.add("sign-up-mode");
});

sign_in_btn.addEventListener('click', () =>{
    container.classList.remove("sign-up-mode");
});


/* SIGN UP */
const user = document.querySelector("#user");
const email = document.querySelector("#email");
const password = document.querySelector("#password");
const buttonUp = document.querySelector("#Sign-Up");
const buttonIn = document.querySelector("#Sign-In");

// Validate Register Form
buttonUp.addEventListener('click',(evento) =>{
    evento.preventDefault();
    var registerUser = user.value;
    var emailUser = email.value;
    var registerPass = password.value;
    if (registerUser == "" || registerPass == "" || emailUser == "") {
        alert("Please Fill All Fields!");
        return false;
    }
    else{
        const endpoint = "http://localhost:5045/user/register"
        data = {
            "username": registerUser,
            "password": registerPass,
            "email": emailUser
        }

        fetch(endpoint,{
            method:'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        })
        .then((res)=>{
            if(res.status === 200){
                window.location.href = "/Index.html"
            }
            else if(res.status === 400){
                throw new Error('The user is already register')
            }
            else{
                throw new Error('Error in the register');
            }
        })
        .catch(error => {
            alert(error.message);
        })
    }
})


/* LOGIN */
const emaillogin = document.querySelector("#email-login");
const passwordlogin = document.querySelector("#password-login");
buttonIn.addEventListener('click', (turism) => {
    turism.preventDefault()
    let loginEmail = emaillogin.value;
    let loginPassword = passwordlogin.value;
    if (loginEmail == "" || loginPassword == "") {
        alert("Please Fill All Fields!");
        return false;
    }
    else{
        const endpoint = "http://localhost:5045/user/Login";
        data = {
            "Email": loginEmail,
            "password": loginPassword
        };
        fetch(endpoint,{
            method:'POST',
            headers:{
                'Content-Type':'application/json'
            },
            body:JSON.stringify(data)
        }).then((res)=> {
            console.log(res);
            if(res.status===200){
                window.location.href="/index.html"
            }
            else if(res.status===400){
                throw new Error("Wrong username or password");
            }
            else if(res.status===401){
                throw new Error("Wrong username or password");
            }
        }).catch(err=>alert(err))
    }
})
