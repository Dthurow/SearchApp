<template>
  <div class="hello">

    <input type="text" v-model="searchquery" /><input type="button" v-on:click="fetchResults" value="search" />
    <div v-if="searchResults.length > 0" class="searchResult" v-for="(item, index) in searchResults">
      <div class="info">
        <div style="margin-bottom:10px;">
          {{item.firstName}} {{item.lastName}}, Age {{item.age}}
        </div>
        Interests are:
        <ul>
          <li v-for="i in item.interests">
            {{i}}
          </li>
        </ul>
        <br />
        Addresses are:
        <ul>
          <li v-for="a in item.addresses">
            <div>
              {{a.streetAddress}}
            </div>
            <div v-if="a.secondaryAddressLine">
              {{a.secondaryAddressLine}}
            </div>
            <div>
              {{a.city}}, {{a.stateOrProvince}} {{a.country}}  {{a.postalCode}}
            </div>
          </li>
        </ul>
      </div>
      <div class="profilePic">
        <img v-bind:src="'/static/' + item.profilePicture" v-if="item.profilePicture" />
      </div>


    </div>
    <div v-if="searchResults.length == 0 && !displayPleaseWait">
      <b>No Results Found...</b>
    </div>
    <div v-if="displayPleaseWait" class="pleaseWait">
      <b>Please Wait...</b>
    </div>
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
      searchquery: '',
      displayPleaseWait: false
    }
  },
  created () {

  },
  methods: {
    fetchResults: function () {
      console.log("searchquery is: '" + this.searchquery + "'")
      this.displayPleaseWait = true
      this.searchResults = []
      axios.get('http://localhost:50256/api/search/GetSubset/' + this.searchquery)
        .then(function (response) {
          this.searchResults = response.data.result;
          this.displayPleaseWait = false;
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

  a {
    color: #42b983;
  }
  .searchResult {
    border: solid 1px black;
    border-radius: 20px;
    margin-top: 20px;
    padding: 20px;
    text-align: left;
  }

  .info{
    display:inline-block;
  }

  .profilePic{
    float:right;
    display:inline-block;
  }
  .profilePic img {
    max-width: 200px;
    max-height: 200px;
  }
  .pleaseWait{
    margin-top:20px;
  }

</style>
