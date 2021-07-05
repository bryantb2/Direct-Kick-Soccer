//class CartClient {
    const bundleCartItems = () => {
        // get all product quantity inputs
        const cartItemsEls = document.getElementsByClassName("qnty_input");

        let itemArr = [];
        for (let i = 0; i < cartItemsEls.length; i++) {
            const currentEl = cartItemsEls[i];
            // make cart item
            const item = {
                Quantity: currentEl.value,
                ItemID: currentEl.getAttribute('data-cartItemId')
            };
            itemArr.push(item);
        }

        return itemArr;
    }

const updateOnServer = async (apiString, cartItems) => {
    await fetch(apiString, {
        method: 'POST',
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify(cartItems)
    });
    location.reload();
}

const updateCartCallback = () => {
    // get api URL
    const apiURL = window.location + "/UpdateCart";
    const cartItems = bundleCartItems();
    updateOnServer(apiURL, cartItems);
}
