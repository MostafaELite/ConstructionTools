app.controller('indexController',
    function ($scope, $http) {
        $scope.loading = true;
        $scope.indexResources = indexResources;

        $http.get(urls.getAllTools)
            .then((response) => {
                $scope.allTools = response.data;
                $scope.loading = false;
                console.log(response);
            });

        let rentingDays,selectedToolId;
        $scope.rent = (toolId) => {
            selectedToolId = toolId;
            Swal.queue([
                {
                    title: indexResources.popupTitle,
                    confirmButtonText: 'Calculate Fees',
                    input: 'number',
                    showLoaderOnConfirm: true,
                    preConfirm: (numberOfRentingDays) => getRentCost(numberOfRentingDays, toolId)
                }
            ])
                .then(sendRentRequest);

        }


        //----------------------------------Handlers--------------------------------------------------

        //Gets the rent cost for a specific tool for a period from the server the number of renting days
        function getRentCost(numberOfRentingDays) {
            rentingDays = numberOfRentingDays;
            debugger;
            return fetch(urls.calculateFees + `${selectedToolId}/${numberOfRentingDays}`)
                .then(response => response.json())
                .then(showRentCost)
                .catch(handleGetRentInfoFailure);
        }

        //Show a Swal modal containing the rent cost retrived from the server 
        function showRentCost(data) {
            Swal.insertQueueStep({
                title: indexResources.feesInfoMessage(data),
                confirmButtonText: 'Rent',
                showCancelButton: true
            });

        }

        //Show a Swal modal in case of error
        function handleGetRentInfoFailure(error) {
            console.log(error);
            Swal.insertQueueStep({
                type: 'error',
                title: 'Unable to calculate fees'
            });
        }

        //Handles the ok click event for the Swal rent cost modal
        function sendRentRequest(result) {
            //User clicked ok and gone to rent and clicked rent button
            if (result.value.length === 2 && result.value[1] == true)
                $http.post(urls.addNewSoppingCartItem + `${selectedToolId}/${rentingDays}`)
                    .then((response) => {
                        response.data == true
                            ? Swal.fire('Done!', indexResources.itemAddedSuccessfuly, 'success')
                            : Swal.fire('Error!', 'Error adding your item , please try again later', 'error');

                    });

        };


    });

