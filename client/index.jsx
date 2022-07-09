//import React from 'react';
import PersonList from "./PersonList";

const rootNode = document.getElementById("app1");    // элемент для рендеринга приложения React
    // получаем корневой элемент 
    const root = ReactDOM.createRoot(rootNode);
    // рендеринг в корневой элемент
    root.render(
        <h1>Hello React</h1>  // элемент, который мы хотим создать
     );
     root.render(
         <PersonList/>
     );