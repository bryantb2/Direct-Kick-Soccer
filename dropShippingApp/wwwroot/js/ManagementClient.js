
const serverAPIURL = 'https://localhost:44386';

const renderProductDropdown = (dropdownElement, productData) => {
    // reset select list
    wipeSelectList(dropdownElement);
    // loop through product data
    productData.forEach(product => {
        const option = document.createElement("option");
        option.value = product.customProductID;
        option.text = "SKU #"
            + product.baseProduct.sku + " " + product.baseProduct.baseSize.sizeName + " "
            + product.baseProduct.baseColor.colorName;
        // add option to select list
        dropdownElement.add(option);
    });
    // show element
    dropdownElement.style.display = "inline-block";
};

const hideProductDropdown = (dropdownElement) => {
    dropdownElement.style.display = "none";
};

const fetchProductsByGroupId = async (groupId) => {
    // get products by group id
    const endpoint = serverAPIURL + '/team/GetProductsByGroupId/' + groupId;
    const response = await fetch(endpoint);

    // convert stream to JSON and return
    const final = await response.json();
    return final;
};

// DOM ELEMENT MANIPULATION FUNCTIONS -------------------->
const getSiblingElement = (currentElement, targetId) => {
    // gets a reference to the product selector that is a sibling of the group selector
    const parent = currentElement.parentElement;
    let foundChild;
    for (let i = 0; i < parent.children.length; i++) {
        const currentChild = parent.children[i];
        if (currentChild.id === String(targetId))
            foundChild = currentChild
    }
    return foundChild;
};

const wipeSelectList = (selectListEl) => {
    for (let i = 1; i < selectListEl.options.length; i++) {
        selectListEl.options[i] = null;
    }
}

const handleGroupSelection = async (e) => {
    // this function handles the behavior for the product dropdown when the group is selected
    // if the selected value is 
    const EMPTY = "-100";
    const target = e.target;
    const selectedValue = target.options[target.selectedIndex].value;
    const productSelector = getSiblingElement(target, "productSelection");
    if (selectedValue == EMPTY) {
        // remove product dropdown
        hideProductDropdown(productSelector);
    }
    else {
        // get the product dropdown
        // get the product JSON data
        // show dropdown with products
        const productData = await fetchProductsByGroupId(selectedValue);
        renderProductDropdown(productSelector, productData);
    }
};