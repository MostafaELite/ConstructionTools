app.controller('indexController',
    function ($scope, $http, $compile) {
        $scope.loading = true;
        $scope.indexResources = indexResources;

        $http.get(urls.getAllTools)
            .then((response) => {
                $scope.allTools = response.data;
                $scope.loading = false;
                console.log(response);
            });

        let rentingDays;
        $scope.rent = (toolId) => {
            Swal.queue([
                {
                    title: indexResources.popupTitle,
                    confirmButtonText: 'Calculate Fees',
                    input: 'number',
                    showLoaderOnConfirm: true,
                    preConfirm: (numberOfRentingDays) => {
                        rentingDays = numberOfRentingDays;
                        debugger;
                        return fetch(urls.calculateFees + `${toolId}/${numberOfRentingDays}`)
                            .then(response => response.json())
                            .then(data =>
                                Swal.insertQueueStep({
                                    title: indexResources.feesInfoMessage(data),
                                    confirmButtonText: 'Rent',
                                    showCancelButton: true
                                })
                            )
                            .catch((e) => {
                                console.log(e);
                                Swal.insertQueueStep({
                                    type: 'error',
                                    title: 'Unable to calculate fees'
                                });
                            });
                    }
                }
            ]).then(result => {
                //User clicked ok and gone to rent and clicked rent button
                if (result.value.length === 2 && result.value[1] == true)
                    $http.post(urls.addNewSoppingCartItem + `${toolId}/${rentingDays}`)
                        .then((response) => {
                            response.data == true
                                ? Swal.fire('Done!', indexResources.itemAddedSuccessfuly, 'success')
                                : Swal.fire('Error!', 'Error adding your item , please try again later', 'error');

                        });

            });
        }


    });

