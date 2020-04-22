class CartClient {
    static bundleCartItems = () => {
        // get all product quantity inputs
        const cartItemsEls = document.getElementsByClassName("qnty_input");

        let itemArr = [];
        for (let i = 0; i < cartItemsEls.length; i++) {
            const currentEl = cartItemsEls[i];
            // make cart item
            const item = {
                quantity: currentEl.value,
                itemId: currentEl.getAttribute('data-cartItemId')
            };
            itemArr.push(item);
        }

        return itemArr;
    }

    static updateOnServer = async (apiString, cartItems) => {
        fetch(apiString, {
            method: 'POST',
            headers: {
                'content-type': 'application/json'
            },
            body: JSON.stringify(cartItems)
        });
    }

    static updateCartCallback = (apiString) => {
        return async () => {
            const cartItems = this.bundleCartItems();
            this.updateOnServer(apiString, cartItems);
        }
    }
}
