//通用設置
Chart.defaults.global.legend.display = false;
// 性別
let gender = document.getElementById('chartGender').getContext('2d');
let chartGender = new Chart(gender, {
    type: 'pie',
    data: {
        labels: ['女性', '男性'],
        datasets: [{
            label: '# 年齡區間',
            data: [70, 20],// 資料放這（％）
            backgroundColor: [
                '#d97f1a',
                '#409599'
            ],
            borderWidth: 0,

        }]
    },
    options: {
        tooltips: {
            mode: 'index',
            intersect: false,
        },
        hover: {
            mode: 'nearest',
            intersect: true
        },
        legend: {
            display: true
        },
        scales: {
            yAxes: [{
                ticks: {
                    min: 0,
                    max: 100,
                    display: false,
                },
                gridLines: {
                    display: false
                },

            }]
        },
    },
});

// 年齡區間
let age = document.getElementById('chartAge').getContext('2d');
let chartAge = new Chart(age, {
    type: 'bar',
    data: {
        labels: ['0-2', '3-4', '18-24', '25-34', '未填寫'],// 篩選項目放這
        datasets: [{
            label: '年齡區間',
            data: [17, 3, 54, 6, 20],// 資料放這（％）
            backgroundColor: [
                '#86d4cd',
                '#409599',
                '#409599',
                '#9e9c9f',
            ],
            borderWidth: 0,

        }]
    },
    options: {
        tooltips: {
            mode: 'index',
            intersect: false,
        },
        hover: {
            mode: 'nearest',
            intersect: true
        },
        scales: {
            yAxes: [{
                ticks: {
                    stepSize: 10,
                    min: 0,
                    max: 60,
                    callback: function (value) {
                        return value + '%';
                    }
                }
            }],

        }
    }
});

// 學歷區間
let edu = document.getElementById('chartEdu').getContext('2d');
let chartEdu = new Chart(edu, {
    type: 'bar',
    data: {
        labels: ['高中職及以下', '大學', '研究所及以上', '未填寫'],// 篩選項目放這
        datasets: [{
            label: '百分比',
            data: [17, 3, 54, 6],// 資料放這（％）
            backgroundColor: [
                '#409599',
                '#409599',
                '#9e9c9f',
            ],
            borderWidth: 0,
        }]
    },
    options: {
        tooltips: {
            mode: 'index',
            intersect: false,
        },
        hover: {
            mode: 'nearest',
            intersect: true
        },
        scales: {
            yAxes: [{
                ticks: {
                    stepSize: 10,
                    min: 0,
                    max: 60,
                    callback: function (value) {
                        return value + '%';
                    }
                },

            }]
        }
    }
});

// 居住地分佈
let live = document.getElementById('chartLive').getContext('2d');
let chartLive = new Chart(live, {
    type: 'horizontalBar',
    data: {
        labels: ['基隆市', '台北市', '新北市', '桃園縣', '新竹市', '新竹縣', '苗栗縣', '台中市', '彰化縣', '南投縣', '雲林縣', '嘉義市', '嘉義縣', '台南市', '高雄市', '屏東縣', '台東縣', '花蓮縣', '宜蘭縣', '澎湖縣', '金門縣', '連江縣'],// 篩選項目放這
        datasets: [{
            label: '百分比',
            data: [17, 3, 54, 6, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20],// 資料放這（％）
            backgroundColor: '#409599',
            borderWidth: 0,
        }]
    },
    options: {
        tooltips: {
            mode: 'index',
            intersect: false,
        },
        hover: {
            mode: 'nearest',
            intersect: true
        },
        scales: {
            yAxes: [{
                ticks: {
                    stepSize: 10,
                    min: 0,
                    max: 60,
                },
            }],
        }
    }
});

// 職業類別分佈
let job = document.getElementById('chartJob').getContext('2d');
let chartJob = new Chart(job, {
    type: 'bar',
    data: {
        labels: ['服務業', '學生', '軍公教', '影視傳播', '未填寫'],// 篩選項目放這
        datasets: [{
            label: '百分比',
            data: [17, 3, 54, 6, 20],// 資料放這（％）
            backgroundColor: [
                '#409599',
                '#409599',
                '#409599',
                '#409599',
                '#9e9c9f',
            ],
            borderWidth: 0,
        }]
    },
    options: {
        tooltips: {
            mode: 'index',
            intersect: false,
        },
        hover: {
            mode: 'nearest',
            intersect: true
        },
        scales: {
            yAxes: [{
                ticks: {
                    stepSize: 10,
                    min: 0,
                    max: 60,
                    callback: function (value) {
                        return value + '%';
                    }
                },

            }]
        }
    }
});