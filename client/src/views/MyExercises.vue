<template>
  <div class="exercises">
    <h4>
      <b>Add an exercise</b>
    </h4>

    <div>
      <vs-input class="margin-bottom" v-model="eName" placeholder="Name">
        <template #icon>
          <i class='bx bxs-rename'></i>
        </template>
      </vs-input>
      <vs-input class="margin-bottom" v-model="eDescription" placeholder="Description">
        <template #icon>
          <i class='bx bxs-credit-card-front' ></i>
        </template>
      </vs-input>
      <vs-input class="margin-bottom" type="url" v-model="eVideoUrl" placeholder="Video url">
        <template #icon>
          <i class='bx bxs-videos' ></i>
        </template>
      </vs-input>
      <vs-select
        class="margin-bottom"
        multiple
        filter
        placeholder="Select body parts"
        v-model="eTargetParts"
        v-if="bodyParts.length > 0"
      >
        <vs-option v-for="bp in bodyParts" :key="bp.id" :label="bp.name" :value="bp">
          {{ bp.name }}
        </vs-option>
      </vs-select>

      <vs-button style="margin: 0;" @click="addExercise()">
        Add Exercise
      </vs-button>
    </div>

    <hr>

    <div v-for="e in exercises" :key="e.id" class="exercise">
      <p id="name"><b>{{ e.name }}</b></p>
      <p id="description">{{ e.description }}</p>
      <a :href="e.videoUrl" target="_blank">Watch video</a>
      <vs-button @click="showBodyParts(e.targetParts)">Show affected body parts</vs-button>
    </div>

    <vs-dialog v-model="showDialog" :loading="loading">
      <div v-for="bp in dialogBodyParts" :key="bp.id" class="body-part">
        <img style="max-width: 100px;" :src="bp.imageUrl" alt="">
        <p><b>{{ bp.name }}</b></p>
        <p>Location: {{ bp.location }}</p>
      </div>
    </vs-dialog>
  </div>
</template>

<script>
import { mapState } from 'vuex';

export default {
  name: 'Exercises',
  data:() => ({
    exercises: [],
    bodyParts: [],
    dialogBodyParts: [],
    showDialog: false,
    loading: true,
    showAddDialog: false,
    eName: '',
    eDescription: '',
    eVideoUrl: '',
    eTargetParts: [],
    user: null,
  }),
  computed: {
    ...mapState([
      'isLoggedIn',
      'account'
    ]),
  },
  methods: {
    getExercises() {
      fetch(
        "https://" + window.location.hostname + ":44369/api/v1/users/" + 1,
        ).then((res) => {
          return res.json()
      }).then((user) => {
        if (user != null) {
          this.user = user;
          console.log(user)
        }
        if (user != null && user.exercises != null) {
          this.exercises = user.exercises;
        }
      })
    },
    async showBodyParts(targetParts) {
      this.loading = true;
      this.dialogBodyParts = [];
      this.showDialog = true;
      for (const tp of targetParts) {
        let res = await fetch("https://" + window.location.hostname + ":44369/api/v1/bodyparts/" + tp.bodyPartId);
        let bp = await res.json();
        this.dialogBodyParts.push(bp);
      };
      this.loading = false;
    },
    async getBodyParts() {
      fetch(
        "https://" + window.location.hostname + ":44369/api/v1/bodyparts",
        ).then((res) => {
          return res.json()
      }).then((bparts) => {
        this.bodyParts = bparts;
      })
    },
    async addExercise() {
      let res = await fetch("https://" + window.location.hostname + ":44369/api/v1/exercises",
      {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          name: this.eName,
          targetParts: this.eTargetParts,
          description: this.eDescription,
          videoUrl: this.eVideoUrl
        }),
      });

      console.log(await res.json())
    }
  },
  mounted() {
    this.getExercises();
    this.getBodyParts();
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