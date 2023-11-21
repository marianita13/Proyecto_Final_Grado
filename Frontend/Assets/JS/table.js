
    var data = localStorage.getItem("storedIds")
    var consult = localStorage.getItem("prueba")
    console.log(data);
    console.log(consult);


    var url = ""
    const localhost = "http://localhost:5045";
    if (consult.includes("Offices")){
        if(data==1){
            url = `${localhost}/Office/GetCitiesWithOffices`
        }
        if(data==2){
            url = `${localhost}/Office/GetCitiesWithOfficesInSpain`
        }
    }
    fetch(`${url}`)
    .then((response) => {
        if(!response.ok){
            throw new Error('No se pudo obtener la información del servidor')
        };
        return response.json()
    })
    .then(users => {
        const keys = Object.keys(users[0]);
        var table = document.querySelector('#example');
        
        // Crear el encabezado (thead)
        const thead = table.createTHead();
        const headerRow = thead.insertRow();
        keys.forEach(key => {
            const th = document.createElement('th');
            th.textContent = key;
            headerRow.appendChild(th);
        });
        
        // Crear el cuerpo (tbody) y llenar la tabla
        const tbody = table.createTBody();
        users.forEach(item => {
            const row = tbody.insertRow();
            keys.forEach(key => {
                const cell = row.insertCell();
                cell.textContent = item[key];
            });
        });

        // Inicializar DataTables después de llenar la tabla
        $(document).ready(function () {
            $('#example').DataTable();
        });
    })
    .catch(error => {
        // Manejar errores y mostrar mensaje de error
        console.error(error.message);
    });
