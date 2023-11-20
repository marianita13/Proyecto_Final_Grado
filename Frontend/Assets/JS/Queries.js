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
    localStorage.clear();
    const storedIds = JSON.parse(localStorage.getItem('storedIds')) || [];

    // Verificar si el ID ya está en la lista
    if (!storedIds.includes(id)) {
        // Si no está en la lista, agregarlo
        storedIds.push(id);

        // Actualizar el localStorage con la nueva lista de IDs
        localStorage.setItem('storedIds', JSON.stringify(storedIds));

        // Puedes hacer algo más aquí si es necesario, como actualizar la interfaz de usuario, etc.
    }
};

// Agregar manejadores de clic a los botones generados
document.querySelectorAll('.Boton').forEach((button) => {
    button.addEventListener('click', () => {
        const id = button.classList[1]; // Obtener el ID del botón a través de las clases
        handleButtonClick(id);
    });
})