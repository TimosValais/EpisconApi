const url = "https://localhost:7121/";
var downloadedProducts = [];
let productsList = document.getElementById('products');
let sortBy = 'title', orderBy = 'asc', page = 1, tableSize = 20, minPrice = 0, maxPrice = 1000;

if (productsList) {
    fetch(url + 'Products/All')
        .then(response => response.json())
        .then(data => {
            downloadedProducts.splice(0, downloadedProducts.length);
            data.forEach(d => downloadedProducts.push(d));
        })
        .catch(ex => {
            alert("Something went wrong ...");
            console.log(ex);
        });
}


let showProductsOnClick = () => {
    
    if (downloadedProducts.length == 0) return;
    productsList.innerHTML = '';
    reDrawTable();
}

let reDrawTable = () => {
    productsList.innerHTML = '';
    downloadedProducts.forEach(product => {
        let item = document.createElement('tr');
        let title = document.createElement('td');
        title.style.maxWidth = '25%';
        title.innerHTML = product.title;
        let price = document.createElement('td');
        price.style.maxWidth = '25%';
        price.innerHTML = product.price;
        let description = document.createElement('td');
        description.style.maxWidth = '25%';
        description.innerHTML = product.description;
        let category = document.createElement('td');
        category.style.maxWidth = '25%';
        category.innerHTML = product.category;
        let image = document.createElement('img');
        image.src = product.image;
        image.width = '150';
        image.height = '150';
        let imageTd = document.createElement('td');
        imageTd.style.maxWidth = '25%';
        imageTd.appendChild(image);
        let rating = document.createElement('td');
        rating.style.maxWidth = '25%';
        rating.innerHTML = product.rating;
        item.appendChild(title);
        item.appendChild(price);
        item.appendChild(description);
        item.appendChild(category);
        item.appendChild(image);
        item.appendChild(rating);
        productsList.appendChild(item);
    })
}

let getProductsFromClick = () => {
    console.log('the equality : ', !!!downloadedProducts.length);
    if (downloadedProducts.length == 0) {
        disableButtons();
        fetch(url + 'Products/SeedInitialData', { method: 'PUT' }).then(response => { alert('data inserted! \n', response); getProductsFromDb();});
    }
    
}

let getProductsFromDb = () => {
    fetch(url + 'Products/All')
        .then(response => response.json())
        .then(data => {
            refreshTable(data);
            alert('data received');
        })
        .catch(ex => {
            alert("Something went wrong ...");
            console.log(ex);
        })
        .finally(() => enableButtons());
}

let refreshTable = data => new Promise(resolve => {
    downloadedProducts.splice(0, downloadedProducts.length);
    data.forEach(d => downloadedProducts.push(d));
    resolve;
})

//let refreshTable = (data) => {
//    downloadedProducts.splice(0, downloadedProducts.length);
//    data.forEach(d => downloadedProducts.push(d));
//}

let disableButtons = () => {
    $('.btn').each((index, element) => $(element).prop('disabled', true));
}
let enableButtons = () => {
    $('.btn').each((index, element) => $(element).prop('disabled', false));
}

$('.fa-solid').on('click', (e) => {
    let element = $(e.target);
    orderBy = changeSortingIcon(element);
    sortByColumn(element, orderBy);
})

$('#applyPrices').on('click', () => {
    minPrice = $('#minPrice').val();
    maxPrice = $('#maxPrice').val();
    getProductsApiCall();
})

let changeSortingIcon = element => {
    if (element.hasClass('fa-arrow-down-a-z')) {
        element.removeClass('fa-arrow-down-a-z');
        element.addClass('fa-arrow-up-a-z');
        return 'desc';
    }
    if (element.hasClass('fa-arrow-up-a-z')) {
        element.removeClass('fa-arrow-up-a-z');
        element.addClass('fa-arrow-down-a-z');
        return 'asc';
    }
}

let getProductsApiCall = () => {

    $.ajax({
        method: "GET",
        url: url + 'Products/All',
        data: { sortBy, orderBy, size : tableSize, page, minPrice, maxPrice }
    })
        .done(function (data) {
            refreshTable(data).then(reDrawTable());
        });
}

$('#tableSize').on('change', (e) => {
    tableSize = $(e.target).val();
    getProductsApiCall();
})
$('#tablePage').on('change', (e) => {
    page = $(e.target).val();
    getProductsApiCall();
})
let sortByColumn = (element) => {
    let columnId = element.closest('th').attr('id');
    switch (columnId) {
        case 'titleHeader':
            sortBy = 'title';
            break;
        case 'priceHeader':
            sortBy = 'price';
            break;
        case 'descriptionHeader':
            sortBy = 'description';
            break;
        case 'categoryHeader':
            sortBy = 'category';
            break;
        case 'imageHeader':
            sortBy = 'image';
            break;
        case 'ratingHeader':
            sortBy = 'rating';
            break;
        default:
            sortBy = 'title';
    }

    getProductsApiCall();

    //$.ajax({
    //    url: url + 'Products/All',
    //    type: "get", //send it through get method
    //    data: {
    //        sortBy
    //    },
    //    done: function (data) {
    //        console.log('received data', data);

    //        //Do Something
    //    },
    //    fail: function (xhr) {
    //        //Do Something to handle error
    //    },
    //    always: function () {

    //    }
    //});
}