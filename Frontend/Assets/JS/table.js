
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
        if(data==3){
            url = `${localhost}/Office/GetOfficesWithClientsInFuenlabrada`
        }
        if(data==4){
            url = `${localhost}/Office/GetOfficeNoSellFruits`
        }
        if(data==5){
            url = `${localhost}/Office/GetCitiesWithOfficesInSpain`
        }
        if(data==6){
            url = `${localhost}/Office/GetCitiesWithOfficesInSpain`
        }
    }
    if(consult.includes("Employee")){
        if(data==1){
            url = `${localhost}/Employee/GetEmployeesByBossCode?bossCode=7`
        }
        if(data==2){
            url = `${localhost}/Employee/GetCEOInformation`
        }
        if(data==3){
            url = `${localhost}/Employee/GetNonSalesRepresentatives`
        }
        if(data==4){
            url = `${localhost}/Employee/GetEmployeesWithManagers`
        }
        if(data==5){
            url = `${localhost}/Employee/GetEmployeeHierarchy`
        }
        if(data==6){
            url = `${localhost}/Employee/GetEmployeeNoOffice`
        }
        if(data==7){
            url = `${localhost}/Employee/GetEmployeeNoClient`
        }
        if(data==8){
            url = `${localhost}/Employee/GetEmployeeNoClientWithOffice`
        }
        if(data==9){
            url = `${localhost}/Employee/GetEmployeeNoOfficeNoClient`
        }
        if(data==10){
            url = `${localhost}/Employee/GetEmployeeNoClientWithBoss`
        }
        if(data==11){
            url = `${localhost}/Summary/GetQuantiyEmployees` //revisAR
            console.log('xd');
        }
        if(data==12){
            url = `${localhost}/Summary/GetQuantiyEmployees` //FALTA
            console.log('xd');
        }
        if(data==13){
            url = `${localhost}/Employee/AlbertoSoriaEmployees`
            console.log('xd');
        }
        if(data==14){
            url = `${localhost}/Summary/Employees_Dont_Represent_Client` //FALTA
            console.log('xd');
        }
    }
    if(consult.includes("Clients")){
        if(data==1){
            url = `${localhost}/Employee/GetUniqueClientCodesWithPaymentsIn2008`
        }
        if(data==2){
            url = `${localhost}/Employee/GetCEOInformation`
        }
        if(data==3){
            url = `${localhost}/Employee/GetNonSalesRepresentatives`
        }
        if(data==4){
            url = `${localhost}/Employee/GetEmployeesWithManagers`
        }
        if(data==5){
            url = `${localhost}/Employee/GetEmployeeHierarchy`
        }
        if(data==6){
            url = `${localhost}/Employee/GetEmployeeNoOffice`
        }
        if(data==7){
            url = `${localhost}/Employee/GetEmployeeNoClient`
        }
        if(data==8){
            url = `${localhost}/Employee/GetEmployeeNoClientWithOffice`
        }
        if(data==9){
            url = `${localhost}/Employee/GetEmployeeNoOfficeNoClient`
        }
        if(data==10){
            url = `${localhost}/Employee/GetEmployeeNoClientWithBoss`
        }
        if(data==11){
            url = `${localhost}/Summary/GetQuantiyEmployees` //revisAR
            console.log('xd');
        }
        if(data==12){
            url = `${localhost}/Summary/GetQuantiyEmployees` //FALTA
            console.log('xd');
        }
        if(data==13){
            url = `${localhost}/Employee/AlbertoSoriaEmployees`
            console.log('xd');
        }
        if(data==14){
            url = `${localhost}/Summary/Employees_Dont_Represent_Client` //FALTA
            console.log('xd');
        }
        if(data==1){
            url = `${localhost}/Employee/GetEmployeesByBossCode?bossCode=7`
        }
        if(data==2){
            url = `${localhost}/Employee/GetCEOInformation`
        }
        if(data==3){
            url = `${localhost}/Employee/GetNonSalesRepresentatives`
        }
        if(data==4){
            url = `${localhost}/Employee/GetEmployeesWithManagers`
        }
        if(data==5){
            url = `${localhost}/Employee/GetEmployeeHierarchy`
        }
        if(data==6){
            url = `${localhost}/Employee/GetEmployeeNoOffice`
        }
        if(data==7){
            url = `${localhost}/Employee/GetEmployeeNoClient`
        }
        if(data==8){
            url = `${localhost}/Employee/GetEmployeeNoClientWithOffice`
        }
        if(data==9){
            url = `${localhost}/Employee/GetEmployeeNoOfficeNoClient`
        }
        if(data==10){
            url = `${localhost}/Employee/GetEmployeeNoClientWithBoss`
        }
        if(data==11){
            url = `${localhost}/Summary/GetQuantiyEmployees` //revisAR
            console.log('xd');
        }
        if(data==12){
            url = `${localhost}/Summary/GetQuantiyEmployees` //FALTA
            console.log('xd');
        }
        if(data==13){
            url = `${localhost}/Employee/AlbertoSoriaEmployees`
            console.log('xd');
        }
        if(data==14){
            url = `${localhost}/Summary/Employees_Dont_Represent_Client` //FALTA
            console.log('xd');
        }
        if(data==11){
            url = `${localhost}/Summary/GetQuantiyEmployees` //revisAR
            console.log('xd');
        }
        if(data==12){
            url = `${localhost}/Summary/GetQuantiyEmployees` //FALTA
            console.log('xd');
        }
        if(data==13){
            url = `${localhost}/Employee/AlbertoSoriaEmployees`
            console.log('xd');
        }
        if(data==14){
            url = `${localhost}/Summary/Employees_Dont_Represent_Client` //FALTA
            console.log('xd');
        }
    }
    if(consult.includes("Products")){
        
    }
    if(consult.includes("Pays")){
        if(data==1){
            url = `${localhost}/Payment/GetPaymentsIn2008ByPaypal`
        }
        if(data==2){
            url = `${localhost}/Payment/GetDistinctPaymentMethods`
        }
        if(data==3){
            url = `${localhost}/Payment/GetNonSalesRepresentatives` //FALTA
        }
        if(data==4){
            url = `${localhost}/Payment/GetPaymentsWithManagers` //FALTA
        }
        if(data==5){
            url = `${localhost}/Payment/GetPaymentsForYear`
        }
    }
    if(consult.includes("Orders")){
        if(data==1){
            url = `${localhost}/Orders/GetOrderStatusList`
        }
        if(data==2){
            url = `${localhost}/Orders/GetDelayedOrders`
        }
        if(data==3){
            url = `${localhost}/Orders/GetOrdersDeliveredEarly`
        }
        if(data==4){
            url = `${localhost}/Orders/GetRejectedOrdersIn2009`
        }
        if(data==5){
            url = `${localhost}/Orders/GetOrdersDeliveredInJanuary`
        }
        if(data==6){
            url = `${localhost}/Orders/GetPaymentsForYear` //FALTA
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
