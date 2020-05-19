<template>
  <div class="calories-finder">
    <input v-model="foodQuery" type="text" name="foodQuery" id="foodQuery" @keyup.enter="getCalories()">
    <button @click="getCalories()">Search</button>
    <div
      class="food-item"
      v-for="f in food"
      :key="f.id"
    >
      <h3>{{ f.shrt_desc }}</h3>
      <p>kcal: {{ f.energ_kcal }}</p>
      <hr>
    </div>
  </div>
</template>

<script>
export default {
  data: () => ({
    foodQuery: null,
    food: [],
  }),
  methods: {
    async getCalories() {
      if (this.foodQuery != null) {
        const loading = this.$vs.loading({
          text: 'Loading...'
        });
        let res = await fetch(`https://food-calorie-data-search.p.rapidapi.com/api/search?keyword=${this.foodQuery}`, {
          "method": "GET",
          "headers": {
            "x-rapidapi-host": "food-calorie-data-search.p.rapidapi.com",
            "x-rapidapi-key": "27d3c292c2mshbac9951e312af6fp1c4871jsndedcb81fe037"
          }
        });
        let data = await res.json();
        this.food = data;
        loading.close();
      }
    }
  }
}
</script>

<style lang="scss" scoped>

</style>