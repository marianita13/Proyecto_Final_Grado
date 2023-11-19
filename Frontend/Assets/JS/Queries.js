document.addEventListener("DOMContentLoaded", () =>{
    var xd = localStorage.getItem("prueba");
    var data = document.querySelector(".info-data");
    console.log(xd);

    let Tarjeta = async() =>{
        let card = await fetch ('/Frontend/Assets/json/cards.json');
        let carta = await card.json();
        console.log(carta);
        if(xd.includes('Employee')){
            data.insertAdjacentHTML("beforeend", /*HTML*/`
            ${carta.employee.map((value) => /*HTML*/`
            <div class="card">
                <div class="head">
                    <div>
                        <p>${value.name}</p>
                    </div>
                </div>
                <div id="back">
                    <button class="Consult ${value.Id}" style="background-color:rgb(111, 209, 255);">${value.button}</button>
                </div>
            </div>
            `).join("")}
            `)
        }
        else if(xd.includes("Offices")){
            data.insertAdjacentHTML("beforeend", /*HTML*/`
            ${carta.offices.map((value) => /*HTML*/`
            <div class="card">
                <div class="head">
                    <div>
                        <p>${value.name}</p>
                    </div>
                </div>
                <div id="back">
                    <button class="Consult ${value.Id}" style="background-color:rgb(245, 241, 33);">${value.button}</button>
                </div>
            </div>
            `).join("")}
            `)
        }
        else if(xd.includes("Clients")){
            data.insertAdjacentHTML("beforeend", /*HTML*/``)
        }
        else if(xd.includes("Products")){
            data.insertAdjacentHTML("beforeend", /*HTML*/``)
        }
        else if(xd.includes("Pays")){
            data.insertAdjacentHTML("beforeend", /*HTML*/`
            ${carta.pays.map((value) => /*HTML*/`
            <div class="card">
                <div class="head">
                    <div>
                        <p>${value.name}</p>
                    </div>
                </div>
                <div id="back">
                    <button class="Consult ${value.Id}" style="background-color:rgb(235, 134, 255);">${value.button}</button>
                </div>
            </div>
            `).join("")}
            `)
        }
        else if(xd.includes("Orders")){
            data.insertAdjacentHTML("beforeend", /*HTML*/`
            ${carta.orders.map((value) => /*HTML*/`
            <div class="card">
                <div class="head">
                    <div>
                        <p>${value.name}</p>
                    </div>
                </div>
                <div id="back">
                    <button class="Consult ${value.Id}" style="background-color:rgb(255, 106, 106);">${value.button}</button>
                </div>
            </div>
            `).join("")}
            `)
        }
    }

    Tarjeta()

    function pruebas(){
        var consultaSidebar = document.querySelectorAll(".consult2")
        
        consultaSidebar.forEach(li =>{
            li.addEventListener('click',(evento)=>{
                evento.preventDefault();
                localStorage.clear();
                let prueba = {}
                if(evento.target.classList.contains('employees')){
                    prueba = {"Id":"Employee"};
                }
                if(evento.target.classList.contains('offices')){
                    prueba = {"Id":"Offices"};
                }
                if(evento.target.classList.contains('clients')){
                    prueba = {"Id":"Clients"};
                }
                if(evento.target.classList.contains('products')){
                    prueba = {"Id":"Products"};
                }
                if(evento.target.classList.contains('pays')){
                    prueba = {"Id":"Pays"};
                }
                if(evento.target.classList.contains('orders')){
                    prueba = {"Id":"Orders"};
                }
                var Json = JSON.stringify(prueba);
                localStorage.setItem("prueba",Json)
                location.assign("/Frontend/Pages/Queries.html")
            })
        })
    }

    pruebas()
})