# ConstructionTools
This Project Consists of Two Parts : <br/>
<ol>
  <li>
    Backend :
    <ol>
      <li>ConstructionTools.Domain : Contains The Domain Model and The Main Entities , Since There Isn't muchBusiness Requirment in this Taks Most The Domain Model Are Anemic , Mutable , and act as a data models only </li>
      <li>
        ConstructionTools.Services : Contains Logic and opreations , act as an orchestrator and manage entity opreation
      </li>
       <li>
        ConstructionTools.Repositroy : Contains repositories responsible for data access
      </li>
      <li>
       ConstructionTools.DataAccess : Contains the context and the config for the database related stuff , db table valdation 
      </li>
       <li>
       ConstructionTools.Api : the api that gets called by the front end , acts as an endpoint to access the services logical opreations 
      </li>
         <li>
       ConstructionTools.Test :Unit Tests 
      </li> 
    </ol>  
  </li>
  <br/>
  <li>
    Frontend :
    <ol>
      <li>ConstructionTools.Frontend : Contains the front end views and client side code  , built with angular js</li>
  </ol>
</li>
</ol>
<hr/>
