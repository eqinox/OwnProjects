let result = null;
let operations = ['*', '/', '-', '+', '=', '.'];

function proceedOperation(event) {
    let regex = /([-]?\d+\.?\d*)\s*([-+*\/]?)\s*(\d+\.?\d*)/;
    let $history = $('#history');
    let $result = $('#result');

    if (event === '.') {
        $history.val($history.val() + event);
        return;
    } else if (event === 'C')  {
        $history.val('');
        $result.val('');
        result = null;
        return;
    } else if (event === 'CE') {
        if (operations.indexOf($history.val()[$history.val().length - 1]) != -1) {
            $history.val($history.val().substring(0, $history.val().length - 1));
        } else {
            $result.val('');
        }        
        return;
    } else if (event !== '=') {
        $history.val($history.val() + event);
    }

    if (operations.indexOf(event) != -1) {        
        if (regex.test($history.val())) {
            let regexArray = regex.exec($history.val());
            console.log($history.val());
            console.log(regexArray);
            let firstNum = (result == null)? Number(regexArray[1]) : result;
            let secondNum = Number(regexArray[3]);
            let operation = regexArray[2];
            if (operation != '')  {
                switch (operation) {
                    case "*": {
                        result = firstNum * secondNum;
                        break;   
                    }
                    case "/": {
                        if (secondNum == 0) {
                            $('.error').text('cannot divide by zero');
                            $('.error').css('visibility', 'visible');
                            setTimeout(() => {
                                $('.error').css('visibility', 'hidden');
                            }, 3000);
                        } else {
                            result = firstNum / secondNum;
                        }                        
                        break;
                    }
                    case "-": {
                        result = firstNum - secondNum;
                        break;
                    }
                    case "+": {
                        result = firstNum + secondNum;
                        break;
                    }
                }                
                $result.val(result);
                $history.val(result);
            }
        }
    }
}
