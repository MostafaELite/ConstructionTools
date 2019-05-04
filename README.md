# ConstructionTools
<h2>Architectural Overview</h2>

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

<h2>Prerequisites</h2>
<ol>
  <li>.Net Core SDK 2.1 to run the Api </li>
  <li>.Net Standered SDK for class liberaries  </li>
<li>SQL LocalDb (or any version of sql server but you will have to change the db connection string in the Api project setting file) </li>
</ol>


<h2>Extra Points Achivement Overview </h2>
<table>
<tr>
  <td>Message-based architecture</td>
  <td> Was thinking of Docker with a RabbitMq image but didn't have time for that :(  </td>
</tr>
  <tr>
  <td>IoC / DI</td>
  <td>Achived Via Microsoft.Extensions.DependencyInjection</td>
</tr>
  <tr>
  <td>Decent-looking UI</td>
  <td>it's not the best but it's not bad at all</td>
</tr>
  <tr>
  <td>Caching</td>
  <td>Cached the index page items , ofcourse can't cach fees :)</td>
</tr>
  
    <tr>
  <td>Class and interaction diagrams</td>
  <td></td>
</tr>

</table>



