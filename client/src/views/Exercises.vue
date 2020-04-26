<template>
  <div class="exercises">
    <div v-for="e in exercises" :key="e.id" class="exercise">
      <p id="name"><b>{{ e.name }}</b></p>
      <p id="description">{{ e.description }}</p>
      <a :href="e.videoUrl" target="_blank">Watch video</a>
      <vs-button @click="showBodyParts(e.targetParts)">Show affected body parts</vs-button>
    </div>

    <vs-dialog v-model="showDialog" :loading="loading">
      
    </vs-dialog>
  </div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'

export default {
  name: 'Home',
  components: {
    HelloWorld
  },
  data:() => ({
    exercises: [],
    showDialog: false,
    loading: true
  }),
  methods: {
    getExercises() {
      fetch(
        "https://" + window.location.hostname + ":44369/api/v1/exercises",
        ).then((res) => {
          return res.json()
      }).then((data) => {
        if (data != null) {
          data.forEach(e => {
            this.exercises.push(e);
          });
        }
      })
    },
    async showBodyParts(targetParts) {
      
    },
  },
  mounted() {
    this.getExercises();
  }
}
</script>

<style lang="scss" scoped>
.exercise {
  padding: 2rem;
  border-radius: 2rem;

  &:nth-child(odd) {
    background-color: #f5f5f5;
  }

  .vs-button {
    margin: 0;
    margin-top: 1rem;
  }
}
</style>