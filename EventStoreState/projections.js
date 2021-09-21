var incomeHandler = (state, evt) => { state.count += evt.data.products.length; };
var outcomeHandler = (state, evt) => { state.count -= evt.data.products.length; };

fromStream('basket-ceda5c05-f314-4f04-91ad-1b8cad4d929e')
	.when({
		$init: () => ({ count: 0 }),
		ProductAdd: incomeHandler,
		ProductRemove: outcomeHandler
	})
.outputState();