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
      >
        <vs-option label="Vuesax" value="1">
          Vuesax
        </vs-option>
        <vs-option label="Vue" value="2">
          Vue
        </vs-option>
        <vs-option label="Javascript" value="3">
          Javascript
        </vs-option>
        <vs-option label="Sass" value="4">
          Sass
        </vs-option>
      </vs-select>

      <vs-button style="margin: 0;" @click="showAddDialog = true">
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

export default {
  name: 'Exercises',
  data:() => ({
    exercises: [],
    dialogBodyParts: [],
    showDialog: false,
    loading: true,
    showAddDialog: false,
    eName: '',
    eDescription: '',
    eVideoUrl: '',
    eTargetParts: [],
  }),
  methods: {
    getExercises() {
      fetch(
        "https://" + window.location.hostname + ":44369/api/v1/users/1",
        ).then((res) => {
          return res.json()
      }).then((user) => {
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