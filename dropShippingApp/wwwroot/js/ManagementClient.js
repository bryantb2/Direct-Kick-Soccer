
// build out base API path
const serverAPIURL = ""; // window.location.replace(window.location.pathname, "");

const renderProductDropdown = (selectorGroupElement, dropdownElement, productData) => {
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
    selectorGroupElement.style.display = "inline";
};

const hideProductDropdown = (selectorGroupElement) => {
    selectorGroupElement.style.display = "none";
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
const getProductSelector = (currentElement, targetId) => {
    // gets a reference to the product selector that is adjacent to the group selector
    const grandParent = currentElement.parentElement.parentElement;
    let foundChild;
    // get ref to product selector
    for (let i = 0; i < grandParent.children.length; i++) {
        const currentChild = grandParent.children[i];
        if (currentChild.id === "productSelectorGroup") {
            // look for target element
            for (let j = 0; j < currentChild.children.length; j++) {
                if (currentChild.children[j].id == String(targetId))
                    foundChild = currentChild.children[j];
            }
        }
    }
    return foundChild;
};

const getProductSelectorGroup = (currentElement) => {
    // gets a reference to the product selector that is adjacent to the group selector
    const grandParent = currentElement.parentElement.parentElement;
    let foundChild;
    // get ref to product selector
    for (let i = 0; i < grandParent.children.length; i++) {
        const currentChild = grandParent.children[i];
        if (currentChild.id === "productSelectorGroup") {
            foundChild = currentChild;
        }
    }
    return foundChild;
}

const wipeSelectList = (selectListEl) => {
    if (selectListEl.options.length > 1) {
        for (let i = 1; i < selectListEl.options.length; i++) {
            selectListEl.options[i] = null;
        }
    }
}

const handleGroupSelection = async (e) => {
    // this function handles the behavior for the product dropdown when the group is selected
    // if the selected value is 
    const EMPTY = "-100";
    const target = e.target;
    const selectedValue = target.options[target.selectedIndex].value;
    const productSelector = getProductSelector(target, "productSelection");
    const productSelectorGroup = getProductSelectorGroup(target);
    if (selectedValue == EMPTY) {
        // remove product dropdown
        hideProductDropdown(productSelectorGroup);
    }
    else {
        // get the product dropdown
        // get the product JSON data
        // show dropdown with products
        const productData = await fetchProductsByGroupId(selectedValue);
        renderProductDropdown(productSelectorGroup, productSelector, productData);
    }
};