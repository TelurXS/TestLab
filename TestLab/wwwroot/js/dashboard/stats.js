const data = [
    { year: 2010, count: 10 },
    { year: 2011, count: 20 },
    { year: 2012, count: 15 },
    { year: 2013, count: 25 },
    { year: 2014, count: 22 },
    { year: 2015, count: 30 },
    { year: 2016, count: 28 },
];

const administratorsList = $("#administrators");

// Projects Line Chart
$.ajax({
    type: "GET",
    dataType: 'json',
    url: `/api/GetProjectsGroupedByDay`,

    success: (response) =>
    {
        new Chart(
            document.getElementById('line'),
            {
                type: 'line',
                options: {
                    responsive: true,
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                },
                data: {
                    labels: response.map(row => new Date(row[0].creationDate).toLocaleDateString()),
                    datasets: [
                        {
                            label: 'Projects count per day',
                            data: response.map(row => row.length)
                        }
                    ]
                }
            }
        );
    },
    error: () => { }
});

// Administratots List
$.ajax({
    type: "GET",
    dataType: 'json',
    url: `/api/GetAdministrators`,

    success: (response) =>
    {
        response.forEach((item) => {
            administratorsList.append($(
                `<li class='list-group-item mb-1 d-flex justify-content-between''>
                    <div>
                        <img src='${item.profileImage}' alt='Profile' class='img-fluid' style='width: 35px' />
                        <span class='ml-2' style='font-size: 1.2rem'>${item.login}</span>
                    </div>
                    <a href='/account/profile?id=${item.id}' class='btn btn-outline-primary text-primary'>Profile</a>
                </li>`
            )) 
        });
    },
    error: () => { }
});

// Projects Types Pie Chart
$.ajax({
    type: "GET",
    dataType: 'json',
    url: `/api/GetProjectsTypesStats`,

    success: (response) => {

        new Chart(
            document.getElementById('pie'),
            {
                type: 'pie',
                options: {
                    responsive: true,
                },
                data: {
                    labels: response.map(row => row.key),
                    datasets: [
                        {
                            label: 'Project Types Total',
                            data: response.map(row => row.value)
                        }
                    ]
                }
            }
        );
    },
    error: () => { }
});

// Posts Bar Chart
$.ajax({
    type: "GET",
    dataType: 'json',
    url: `/api/GetPostsGroupedByDay`,

    success: (response) => {

        console.dir(response);

        new Chart(
            document.getElementById('bar'),
            {
                type: 'bar',
                options: {
                    responsive: true,
                },
                data: {
                    labels: response.map(row => new Date(row.key).toLocaleDateString()),
                    datasets: [
                        {
                            label: 'Posts count per day',
                            data: response.map(row => row.value)
                        }
                    ]
                }
            }
        );
    },
    error: () => { }
});