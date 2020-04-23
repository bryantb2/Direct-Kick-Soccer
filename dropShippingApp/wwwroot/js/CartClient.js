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

const updateOnServer = async (apiString) => {
    await fetch(apiString, {
        method: 'POST',
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify(cartItems)
    });
    location.reload();
}

const updateCartCallback = (apiString) => {
    const cartItems = bundleCartItems();
    updateOnServer(apiString, cartItems);
}
