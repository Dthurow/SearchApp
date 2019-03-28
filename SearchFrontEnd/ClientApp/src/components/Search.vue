<template>
  <div class="hello">
    <img src="../assets/logo.png" />
    <input type="text" v-model="searchquery" /><input type="button" v-on:click="fetchResults" value="search" />
    <!--<div class="memoryLine" v-for="(item, index) in searchResults">
    </div>-->
  </div>
</template>

<script>
/*global searchResults searchquery*/
import axios from 'axios'
export default {
  name: 'Search',
  data () {
    return {
      searchResults: [],
      searchquery: ''
    }
  },
  created () {

  },
  methods: {
    fetchResults: function () {
      console.log("searchquery is: '" + this.searchquery + "'")
      axios.get('https://localhost:50256/api/search/getsearchsubset/' + this.searchquery)
        .then(function (response) {
          console.log('hi')
          console.log(response.data)
          this.searchResults = response.data
          console.log('hello')
        }.bind(this))
        .catch(function (error) {
          console.log(error)
        })
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  h1, h2 {
    font-weight: normal;
  }

  ul {
    list-style-type: none;
    padding: 0;
  }

  li {
    display: inline-block;
    margin: 0 10px;
  }

  a {
    color: #42b983;
  }
</style>
