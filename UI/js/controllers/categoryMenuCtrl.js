var categoryMenuCtrl = (function () {
	return {
		render: function () {
			Parse.initialize("BxC62zFfCXJAfLxS90r6hwNSz0OIKtDlZ1sVeCCV", "Av5f9x57L6qsWpxohLSaXtqUD32Pblzm4dyUnYaJ");

			var Offer = Parse.Object.extend('Offer');
			var query = new Parse.Query(Offer);

			query.find({
				success: function (offers) {
					var categoryMenuTemplate = $('#category-menu').html(),
						container = $('#category-menu-container'),
						categories = [];

					offers.forEach(function (offer) {
						if (categories.indexOf(offer._serverData.Category) === -1) {
							categories.push(offer._serverData.Category);
						}
					})

					var outputCatMenuHtml = Mustache.render(categoryMenuTemplate, categories);
					container.append(outputCatMenuHtml)
				}
			});
		}
	}
} ());