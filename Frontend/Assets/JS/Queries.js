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
            <option value="1">2008Payment</option>
            <option value="2">Opción 2</option>
            <option value="3">Opción 3</option>
            <option value="4">Opción 4</option>
            <option value="5">Opción 5</option>
            <option value="6">Opción 6</option>
            <option value="7">Opción 7</option>
            <option value="8">Opción 8</option>
            <option value="9">Opción 9</option>
            <option value="10">Opción 10</option>
            <option value="11">Opción 11</option>
            <option value="12">Opción 12</option>
            <option value="13">Opción 13</option>
            <option value="14">Opción 14</option>
            <option value="15">Opción 15</option>
            <option value="16">Opción 16</option>
            <option value="17">Opción 17</option>
            <option value="18">Opción 18</option>
            <option value="19">Opción 19</option>
            <option value="20">Opción 20</option>
            <option value="21">Opción 21</option>
            <option value="22">Opción 22</option>
            <option value="23">Opción 23</option>
            <option value="24">Opción 24</option>
            <option value="25">Opción 25</option>
            <option value="26">Opción 26</option>
            <option value="27">Opción 27</option>
            <option value="28">Opción 28</option>
            <option value="29">Opción 29</option>
            <option value="30">Opción 30</option>
            <option value="31">Opción 31</option>
            <option value="32">Opción 32</option>
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
