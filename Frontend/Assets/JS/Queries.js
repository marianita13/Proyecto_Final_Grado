document.addEventListener("DOMContentLoaded", () =>{
    var xd = localStorage.getItem("prueba");
    var data = document.querySelector(".info-data");
    console.log(xd);

    let Tarjeta = async() =>{
        let card = await fetch ('/Frontend/Assets/json/cards.json');
        let carta = await card.json();
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
                    <button class="Boton ${value.Id}" style="background-color:rgb(111, 209, 255);"
                    onclick="handleButtonClick('${value.Id}')">${value.button}</button>
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
                    <button class="Boton ${value.Id}" style="background-color:rgb(245, 241, 33);"
                    onclick="handleButtonClick('${value.Id}')">${value.button}</button>
                </div>
            </div>
            `).join("")}
            `)
        }
        else if(xd.includes("Clients")){
            data.insertAdjacentHTML("beforeend", /*HTML*/`
            <select>
            <option value="#"></option>
            <option value="1">Clients from Spain</option>
            <option value="2">Client Id OF 2008 Payments</option>
            <option value="3">Client from Madrid / Sells agent code == 11 or 30</option>
            <option value="4">Info Clients / Sells Agent</option>
            <option value="5">Clients Payments(yes) / Sells Agent</option>
            <option value="6">Clients Payments(no) / Sells Agent</option>
            <option value="7">Clients Payments(yes) / Sells Agent Name / City Office</option>
            <option value="8">Clients Payments(no) / Sells Agent Name / City Office</option>
            <option value="9">Clients / Sells Agent Name / City Office</option>
            <option value="10">Clients who don´t have receive their order already</option>
            <option value="11">Clients Payments(no)</option>
            <option value="12">Clients Orders(no)</option>
            <option value="13">Clients Payments(no) / Clients Orders(no)</option>
            <option value="14">Clients Payments(no) / Clients Orders(yes)</option>
            <option value="15">Quantity Clients for Country</option>
            <option value="16">Quantity Clients</option>
            <option value="17">Clients in Madrid</option>
            <option value="18">Clients in Cities start "M"</option>
            <option value="19">Quantity Clients / Sells Agent(no)</option>
            <option value="20">Client with more Limit Credit</option>
            <option value="21">Clients / Limit Credit > Payments</option>
            <option value="22">Clients Payments(yes)</option>
            <option value="23">Client / Quantity Payments</option>
            <option value="24">Client / Total Payments</option>
            <option value="25">Clients name / Orders in 2008</option>
            <option value="26">Clients name / Sells Agent Name / Phone Office </option>
            </select>
            `)
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
                    <button class="Boton ${value.Id}" style="background-color:rgb(235, 134, 255);"
                    onclick="handleButtonClick('${value.Id}')">${value.button}</button>
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
                    <button class="Boton ${value.Id}" style="background-color:rgb(255, 106, 106);"
                    onclick="handleButtonClick('${value.Id}')">${value.button}</button>
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

function handleButtonClick(id){
    // Obtener los IDs existentes del localStorage o inicializar un array vacío
    localStorage.setItem('storedIds',id)
    const storedIds = JSON.parse(localStorage.getItem('storedIds'));
    console.log(storedIds);
    // Verificar si el ID ya está en la lista
    localStorage.setItem('storedIds', JSON.stringify(storedIds));
    window.location.href = "/Frontend/Pages/Tables.html"
};

// Agregar manejadores de clic a los botones generados
document.querySelectorAll('.Boton').forEach((button) => {
    button.addEventListener('click', () => {
        const id = button.classList[1]; // Obtener el ID del botón a través de las clases
        console.log(id);
        handleButtonClick(id);
    });
})
