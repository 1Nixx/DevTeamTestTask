function LoadShops(optionValueId){
    fetch("/api/shop/all")
        .then((response) => response.json())
        .then((data) => {
            let selectMain = document.getElementById("shop-select");

            for (i = 0; i < data.length; i++) {
                let option = document.createElement("option");
                option.value = data[i].id;
                option.text = data[i].name;

                if (data[i].id === optionValueId)
                    option.setAttribute("selected", "selected");

                selectMain.add(option, null);
            }
        });
}