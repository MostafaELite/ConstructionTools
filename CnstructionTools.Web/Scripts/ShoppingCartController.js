app.controller('shoppingCartController',
    function ($scope, $http) {
        $scope.loading = true;
        $scope.shoppingCartResources = shoppingCartResources;

        $http.get(urls.getShoppingCartItems)
            .then((response) => {
                $scope.shoppingCartItems = response.data;
                $scope.loading = false;
                $scope.sum = 0;
                debugger;
                $scope.sum = $scope.shoppingCartItems.map((item) => $scope.sum += item.cost).sum();
                console.log(response);
            });

        $scope.checkout = () => {
            location.href = urls.checkout;
            window.reload();
        }
    });



