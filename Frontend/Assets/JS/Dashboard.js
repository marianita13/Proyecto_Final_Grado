/*GRAPHIC ONE*/
function graphica1(){
	var dom = document.getElementById('content-data');
    var myChart = echarts.init(dom, null, {
    renderer: 'canvas',
    useDirtyRect: false
    });
    var app = {};
    
    var option;

    option = {
	title: {
		text: 'Orders Status',
		subtext: 'Database Data',
		left: 'center',
		top:'40px'
	},
	tooltip: {
		trigger: 'item'
	},
	legend: {
		orient: 'vertical',
		left: '20px',
		top:'40px'
	},
	series: [
		{
			name: 'Access From',
			type: 'pie',
			top: '100px',
			radius: '50%',
			data: [
				{ value: 65, name: 'Entregado' },
				{ value: 33, name: 'Pendiente' },
				{ value: 32, name: 'Rechazado' }
			],
			emphasis: {
				itemStyle: {
				shadowBlur: 10,
				shadowOffsetX: 0,
				shadowColor: 'rgba(0, 0, 0, 0.5)'
				}
			}
		}
	]
	};
	if (option && typeof option === 'object') {
		myChart.setOption(option);
		}
		
		window.addEventListener('resize', myChart.resize);
}
graphica1()


/*GRAPHIC TWO*/
function graphic2(){
	var dom = document.getElementById('other-graphic');
	var myChart = echarts.init(dom, null, {
	renderer: 'canvas',
	useDirtyRect: false
	});
	var app = {};

	var option;

	option = {
		tooltip: {
			trigger: 'axis',
			axisPointer: {
				type: 'shadow'
			}
		},
		legend: {
			top:'10px'
		},
		grid: {
			left: '3%',
			top:'50px',
			right: '4%',
			bottom: '6%',
			containLabel: true
		},
		xAxis: [
			{
				type: 'category',
				data: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul','Aug','Sep','Oct','Nov','Dec']
			}
		],
		yAxis: [
			{
				type: 'value'
			}
		],
		series: [
			{
				name: 'Orders',
				color: '#FC3B56',
				type: 'bar',
				emphasis: {
				focus: 'series'
				},
				data: [39,13,19,8,5,3,2,3,1,8,6,9]
			}
		]
	};

	if (option && typeof option === 'object') {
		myChart.setOption(option);
	}

	window.addEventListener('resize', myChart.resize);
}
graphic2()

document.addEventListener('DOMContentLoaded', function(){
	consultPrincipal()
	pruebas()
})
function consultPrincipal(){
	var consulta = document.querySelectorAll('.Consult');
	consulta.forEach(button =>{
		button.addEventListener('click',(evento)=>{
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