<template>
  <div id="app">
    <h4>Person Search Application</h4>
    <p>
      Search for a person of interest by using a piece of their first or last name.
      Some suggested searches: Peggy, ar, laura
    </p>
    <router-view/>
  </div>
</template>

<script>
export default {
  name: 'App'
}
</script>

<style>
  #app {
    font-family: 'Avenir', Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
    width: 500px;
    margin: auto;
    margin-bottom:100px;
  }
</style>
