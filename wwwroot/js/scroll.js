function ScrollToBottom(elementName){
    element = document.getElementById(elementName);
    element.scrollTop = element.scrollHeight;
    console.log("scrolling...");
}