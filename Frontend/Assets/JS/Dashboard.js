/*GRAPHIC ONE*/
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

/*GRAPHIC TWO*/

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

